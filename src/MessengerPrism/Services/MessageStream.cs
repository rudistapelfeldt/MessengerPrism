using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;

namespace MessengerPrism
{
    public class MessageStream
    {
        public static Dictionary<string, string> MessageTypes { get; set; }

        private static List<Func<string>> _generators;

        private Action<string> _handler;

        private static Random random = new Random();

        public string JsonString { get; set; }

        public MessageStream()
        {
            MessageTypes = new Dictionary<string, string>()
            {
                { "DeliveryReport", "{\"mtype\": \"delivery_report\", \"msg_id\":\"MSGID\", \"recipient_id\":\"USERID\", \"ts\":TIME}" },
                { "GroupCreated", "{\"mtype\": \"group_created\", \"group_id\":\"GRPID\", \"group_name\":\"GRPNAME\"}" },
                { "ImageMessage", "{\"mtype\": \"image_message\", \"msg_id\":\"MSGID\",\"sender_id\":\"USERID\", \"group_id\":\"GRPID\", \"url\":\"IMGURL\", \"ts\":TIME }" },
                { "TextMessage", "{\"mtype\": \"text_message\", \"msg_id\":\"MSGID\", \"sender_id\":\"USERID\", \"group_id\":\"GRPID\", \"message\":\"MSG\", \"ts\":TIME}" },
                { "UserRegistered", "{\"mtype\": \"user_registered\", \"user_id\":\"USERID\", \"name\":\"FULLNAME\"}" }
            };

            _generators = new List<Func<string>>()
            {
                userRegisteredGenerator,
                textMessageGenerator,
                imageMessageGenerator,
                groupCreatedGenerator,
                userRegisteredGenerator,
                textMessageGenerator,
                userRegisteredGenerator,
                textMessageGenerator,
            };
        }

        private Timer tmrPubisher;
        public void Subscribe(Action<string> handler)
        {
            _handler = handler;

            tmrPubisher = new Timer(TimeSpan.FromSeconds(2).TotalMilliseconds);
            tmrPubisher.Elapsed += (sender, args) => GenerateEvent();
            tmrPubisher.AutoReset = false;
            tmrPubisher.Start();
        }

        public Task<string> Readline()
        {
            var _tcs = new TaskCompletionSource<string>();

            EventHandler handler = null;
            handler = new EventHandler((sender, args) =>
            {
                var entry = sender as Entry;
                _tcs.SetResult(entry.Text);
                entry.Text = string.Empty;
                entry.Completed -= handler;
            });

            return _tcs.Task;
        }

        private void GenerateEvent()
        {
            tmrPubisher.Interval = random.Next(1,4000);
            tmrPubisher.Start();

            if (_generators.Any() == false) return;

            Task.Run(() =>
            {
                try
                {
                    _handler?.Invoke(_generators[random.Next(_generators.Count)].Invoke());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            });
        }



        static string ReadMessageType(string messagetype)
        {
            try
            {
                string content;

                content = MessageTypes[messagetype];
                
                return content;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        static List<string> _users = new List<string>()
        {
            "Harold",
            "Jim",
            "Albert",
            "Martin",
            "John",
            "Pete",
            "Hank",
            "Joe",
            "Roger",
            "Henny"
        };


        private static List<string> _groupIds = ((Func<List<string>>)(() =>
        {
            var groupIds = new List<string>();
            for (var i = 0; i < 10; i++)
            {
                groupIds.Add(Guid.NewGuid().ToString());
            }

            return groupIds;
        }))();

        private static List<string> _groupCreationGroupIds = _groupIds.Select(n => n).ToList();



        private static readonly Func<string> userRegisteredGenerator = () =>
        {
            lock (_users)
            {
                var user = _users.First();
                _users.Remove(user);
                if (_users.Any() == false) _generators.Remove(userRegisteredGenerator);

                return ReadMessageType("UserRegistered").Replace("USERID", (9 - _users.Count).ToString())
                    .Replace("FULLNAME", user);
            }
        };

        private static readonly Func<string> groupCreatedGenerator = () =>
        {
            lock (_groupCreationGroupIds)
            {
                var groupId = _groupCreationGroupIds.First();
                _groupCreationGroupIds.Remove(groupId);
                if (_groupCreationGroupIds.Any() == false) _generators.Remove(groupCreatedGenerator);

                return ReadMessageType("GroupCreated")
                    .Replace("GRPID", groupId)
                    .Replace("GRPNAME", $"Group Chat {(10 - _groupCreationGroupIds.Count)}");
            }
        };

        private static Func<string> textMessageGenerator = () =>
        {
            return ReadMessageType("TextMessage")
                .Replace("USERID", random.Next(10).ToString())
                .Replace("MSGID", Guid.NewGuid().ToString())
                .Replace("GRPID", _groupIds[random.Next(10)])
                .Replace("MSG", "This is a text message")
                .Replace("TIME", DateTime.Now.Ticks.ToString());
        };

        private static Func<string> imageMessageGenerator = () =>
        {
            var imageUrl = "https://www.petmd.com/sites/default/files/CANS_HamsterSign_729603697%20(1).jpg";

            return ReadMessageType("ImageMessage")
                .Replace("USERID", random.Next(10).ToString())
                .Replace("MSGID", Guid.NewGuid().ToString())
                .Replace("GRPID", _groupIds[random.Next(10)])
                .Replace("IMGURL", imageUrl)
                .Replace("TIME", DateTime.Now.Ticks.ToString());
        };


        /// <summary>
        /// Simulates sending a message. Returns whether the send was successful or not.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="groupId"></param>
        /// <returns>Whether the send was successful</returns>
        public bool Send(string groupId, string message)
        {
            Task.Delay(3000).Wait();
            return random.Next(2) > 0;
        }
    }


}