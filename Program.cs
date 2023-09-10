using EleCho.GoCqHttpSdk;
using EleCho.GoCqHttpSdk.Action;
using EleCho.GoCqHttpSdk.Message;
using System;
using System.Security.Cryptography.X509Certificates;
using static System.Collections.Specialized.BitVector32;

namespace LuzhouBot
{

    class Program
    {

        static async Task Main(string[] args)
        {

            await ress();
            // 这里可以添加其他逻辑或等待连接的代码
            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();
        }

        public static async Task ress()
        {
            // 初始化一个 CqWsSession 用来与 Go-CqHttp 通信
            CqWsSession session = new CqWsSession(new CqWsSessionOptions()
            {
                BaseUri = new Uri("ws://c4051f4b-7b82-ec78-90e1-def8cea13ea7.openfrp.cc:35049"),  // WebSocket 地址
            });

            try
            {
                Console.WriteLine("尝试连接Cqhttp框架");
                session.Start(); // 开始连接 (你也可以使用它的异步版本)
                Console.WriteLine("连接Cqhttp框架成功");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine("Press Enter to exit...");
                Console.ReadLine();
            }
            Console.WriteLine("请输入需要发送的QQ群号");
            string qq = "";
            qq = Console.ReadLine();
            long qqnumber = Convert.ToInt64(qq);
            CqSendGroupMessageAction actionstart = new CqSendGroupMessageAction(qqnumber, new CqMessage("LuzhouBot已连接====>"));
            CqActionResult? rstsend = await session.ActionSender.InvokeActionAsync(actionstart);
            string exit = "";
            while (exit != "exit")
            {
                string sendtext = null;
                Console.WriteLine("请输入需要发送的文本");
                sendtext = Console.ReadLine();
                string replacedInput = sendtext.Replace("\\n", Environment.NewLine);
                if (sendtext != "")
                {
                    if (sendtext != "exit")
                    {
                        // 新建一个发送私聊消息的操作
                        CqSendGroupMessageAction actionsend = new CqSendGroupMessageAction(qqnumber, new CqMessage(replacedInput));

                        CqActionResult? rstsendsend = await session.ActionSender.InvokeActionAsync(actionsend);
                    }
                    else
                    {
                        exit = "exit";
                    }
                }
                else
                {
                    Console.WriteLine("消息不能为空");
                }
            }

            CqSendGroupMessageAction actionend = new CqSendGroupMessageAction(qqnumber, new CqMessage("LuzhouBot已退出"));
            CqActionResult? rstend = await session.ActionSender.InvokeActionAsync(actionend);
        }
    }
}