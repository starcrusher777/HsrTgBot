using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;
using static train.Commands;


namespace train
{
    internal class Program
    {
        private static Dictionary<long, int> _userAnswers = new();
        private static ITelegramBotClient bot = new TelegramBotClient("token");
        private static List<(long, string)> stages = new List<(long, string)>();
        private static Dictionary<long, List<string>> Answers = new();
        private static List<string> promos = new List<string>();

        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            long? numbersChatId = null;
            long? daysChatId = null;
            var message = update.Message;
            long chatId = message.Chat.Id;
            
            if (message == null || message.Type != Telegram.Bot.Types.Enums.MessageType.Text)
                return;
            
            if (!Answers.ContainsKey(chatId))
            {
                Answers[chatId] = new List<string>();
            }
            
            if (_userAnswers.ContainsKey(chatId))
            {
                var currentQuestion = _userAnswers[chatId];
                switch (currentQuestion)
                {
                    case 0:
                        break;
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        _userAnswers.Remove(chatId);
                        break;
                }
            }
            
            
            var chatStage = await getChatStage(message.Chat.Id);
            if (!string.IsNullOrEmpty(chatStage))
            {
                switch (chatStage)
                {
                    case CountJumps: await HandleReceivedNumbersAsync(message);
                        break;
                    case PatchJade:
                        break;
                    case hasPass : 
                        break;
                    case noPass : 
                        break;
                    case hasBP : 
                        break;
                    case noBP : 
                        break;
                    case F2P: await HandleReceivedDaysAsyncFTP(message);
                        break;
                    case NotF2P : await HandleReceivedDaysAsync(message);
                        break;
                    case "I" : 
                        break;
                    case "II" : 
                        break;
                    case "III" : 
                        break;
                    case "IV" :
                        break;
                    case "V" : 
                        break;
                    case "VI" : 
                        break;
                    case "VII" : 
                        break;
                    case "VIII" : 
                        break;
                    case "IX" : 
                        break;
                    case "X" : 
                        break;
                    case "XI" :
                        break;
                    case "XII" :
                        break;
                    case "1" :
                        break;
                    case "2" :
                        break;
                    case "3" :
                        break;
                    case "4" :
                        break;
                    case "days":
                        break;
                }
                return;
            }

            if (message.Text == CountJumps)
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Вы выбрали посчитать прыжки, введите колличество нефрита");
                await setChatInStage(message.Chat.Id, CountJumps);
                return;
            }
            
            if (message.Text == PatchJade)
            {
                _userAnswers[chatId] = 0;
                await botClient.SendTextMessageAsync(message.Chat.Id, "Вы выбрали показать колличество нефрита за патч");
                await setChatInStage(chatId, PatchJade);
                await SendQuestion1(chatId, update);
            }

            if (message.Text == CountJade)
            {
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Вы выбрали посчитать колличество нефрита через X дней, выбирите нужный вариант");
                    var replyKeyboard2 = new ReplyKeyboardMarkup(
                        new List<KeyboardButton[]>()
                        {
                            new KeyboardButton[]
                            {
                                new(NotF2P),
                                new(F2P)
                            },
                        })
                    {
                        ResizeKeyboard = true
                    };
                    await botClient.SendTextMessageAsync(message.Chat, "Есть ли у вас пропуск?",
                
                        replyMarkup: replyKeyboard2);
            }

            if (message.Text == F2P)
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Введите колличество дней");
                await setChatInStage(message.Chat.Id, F2P);
                return;   
            }

            if (message.Text == NotF2P)
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Введите колличество дней");
                await setChatInStage(message.Chat.Id, NotF2P);
                return; 
            }
            if (message.Text == hasPass) 
            {
                _userAnswers[chatId] = 1;
                Console.WriteLine($"Установлено значение _userAnswers[{chatId}] = {_userAnswers[chatId]}"); //debug
                await setChatInStage(chatId, hasPass);
                await ProcessAnswer1(chatId, update);
                await SendQuestion2(chatId, update);
                
            }

            if (message.Text == noPass)
            {
                _userAnswers[chatId] = 1;
                
                await setChatInStage(chatId, noPass);
                await ProcessAnswer1(chatId, update);
                await SendQuestion2(chatId, update);
                
            }
            
            if (message.Text == hasBP)
            {
                _userAnswers[chatId] = 2;
                await setChatInStage(chatId, hasBP);
                await ProcessAnswer2(chatId, update);
                await SendQuestion3(chatId, update);
                
            }

            if (message.Text == noBP)
            {
                _userAnswers[chatId] = 2;
                await setChatInStage(chatId, noBP);
                await ProcessAnswer2(chatId, update);
                await SendQuestion3(chatId, update);
                
            }
            
            if (message.Text == "I")
            {
                _userAnswers[chatId] = 3;
                await setChatInStage(chatId, "I");
                await ProcessAnswer3(chatId, update);
                await SendQuestion4(chatId, update);
                
            }
            
            if (message.Text == "II")
            {
                _userAnswers[chatId] = 3;
                await setChatInStage(chatId, "II");
                await ProcessAnswer3(chatId, update);
                await SendQuestion4(chatId, update);
            }
            
            if (message.Text == "III")
            {
                _userAnswers[chatId] = 3;
                await setChatInStage(chatId, "III");
                await ProcessAnswer3(chatId, update);
                await SendQuestion4(chatId, update);
            }
            
            if (message.Text == "IV")
            {
                _userAnswers[chatId] = 3;
                await setChatInStage(chatId, "IV");
                await ProcessAnswer3(chatId, update);
                await SendQuestion4(chatId, update);
            }
            
            if (message.Text == "V")
            {
                _userAnswers[chatId] = 3;
                await setChatInStage(chatId, "V");
                await ProcessAnswer3(chatId, update);
                await SendQuestion4(chatId, update);
            }
            
            if (message.Text == "VI")
            {
                _userAnswers[chatId] = 3;
                await setChatInStage(chatId, "VI");
                await ProcessAnswer3(chatId, update);
                await SendQuestion4(chatId, update);
            }
            
            if (message.Text == "VII")
            {
                _userAnswers[chatId] = 3;
                await setChatInStage(chatId, "VII");
                await ProcessAnswer3(chatId, update);
                await SendQuestion4(chatId, update);
            }
            
            if (message.Text == "VIII")
            {
                _userAnswers[chatId] = 3;
                await setChatInStage(chatId, "VIII");
                await ProcessAnswer3(chatId, update);
                await SendQuestion4(chatId, update);
            }
            
            if (message.Text == "IX")
            {
                _userAnswers[chatId] = 3;
                await setChatInStage(chatId, "IX");
                await ProcessAnswer3(chatId, update);
                await SendQuestion4(chatId, update);
            }
            
            if (message.Text == "X")
            {
                _userAnswers[chatId] = 3;
                await setChatInStage(chatId, "X");
                await ProcessAnswer3(chatId, update);
                await SendQuestion4(chatId, update);
            }
            
            if (message.Text == "XI")
            {
                _userAnswers[chatId] = 3;
                await setChatInStage(chatId, "XI");
                await ProcessAnswer3(chatId, update);
                await SendQuestion4(chatId, update);
            }
            
            if (message.Text == "XII")
            {
                _userAnswers[chatId] = 3;
                await setChatInStage(chatId, "XII");
                await ProcessAnswer3(chatId, update);
                await SendQuestion4(chatId, update);
            }

            if (message.Text == "1")
            {
                _userAnswers[chatId] = 4;
                await setChatInStage(chatId, "1");
                await ProcessAnswer4(chatId, update);
                await TotalJadePatch(chatId, message);
            }
            
            if (message.Text == "2")
            {
                _userAnswers[chatId] = 4;
                await setChatInStage(chatId, "2");
                await ProcessAnswer4(chatId, update);
                await TotalJadePatch(chatId, message);
            }
            
            if (message.Text == "3")
            {
                _userAnswers[chatId] = 4;
                await setChatInStage(chatId, "3");
                await ProcessAnswer4(chatId, update);
                await TotalJadePatch(chatId, message);
            }
            
            if (message.Text == "4")
            {
                _userAnswers[chatId] = 4;
                await setChatInStage(chatId, "4");
                await ProcessAnswer4(chatId, update);
                await TotalJadePatch(chatId, message);
            }
            

            if (message.Text == calcs)
            {
                var replyKeyboard = new ReplyKeyboardMarkup(
                    new List<KeyboardButton[]>()
                    {
                        new KeyboardButton[]
                        {
                            new(CountJumps),
                            new(PatchJade),
                            new(CountJade)
                        }
                    })
                    
                {
                    ResizeKeyboard = true
                };
                await botClient.SendTextMessageAsync(message.Chat, "Что вас интересует?",
                    replyMarkup: replyKeyboard);    
            }

            if (message.Text == showPromos)
            {
                var promo1 = "SB87P52BTVQ3";
                var desc1 = "\u00d750 звездного нефрита, \u00d710 000 кредитов";
                
                var promo2 = "6B976L2STURF";
                var desc2 = "\u00d750 звездного нефрита, \u00d710 000 кредитов";
                
                var promo3 = "SWEETDREAMS";
                var desc3 = "\u00d72 очищенный эфир, \u00d72 путеводитель путешественника";
                
                
                await botClient.SendTextMessageAsync(message.Chat, "hsr.hoyoverse.com/gift");
                
                await botClient.SendTextMessageAsync(message.Chat, $"{promo1}");
                await botClient.SendTextMessageAsync(message.Chat, $"{desc1}");
                
                await botClient.SendTextMessageAsync(message.Chat, $"{promo2}");
                await botClient.SendTextMessageAsync(message.Chat, $"{desc2}");
                
                
                var replyKeyboard = new ReplyKeyboardMarkup(
                    new List<KeyboardButton[]>()
                    {
                        new KeyboardButton[]
                        {
                            new(calcs),
                            new(showPromos)
                        },
                    })
                    
                {
                    ResizeKeyboard = true,
                };
                await botClient.SendTextMessageAsync(message.Chat, $"{promo3}",
                    replyMarkup: replyKeyboard);
                await botClient.SendTextMessageAsync(message.Chat, $"{desc3}");
                
            }


            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
           
            if (update.Type == UpdateType.Message)
            {
                
                if (message.Text.ToLower() == "/start")
                {
                    var welcomeMessage = "Привет первопроходец, с помощью этого бота ты сможешь узнать актуальные промокоды, посчитать колличество прыжков и звездного нефрита.";
                    await botClient.SendTextMessageAsync(message.Chat, $"{welcomeMessage}");
                    var replyKeyboard = new ReplyKeyboardMarkup(
                        new List<KeyboardButton[]>()
                        {
                            new KeyboardButton[]
                            {
                                new(calcs),
                                new(showPromos)
                            }
                        })
                    
                    {
                        ResizeKeyboard = true,
                    };
                    await botClient.SendTextMessageAsync(message.Chat, "Что вас интересует?",
                        replyMarkup: replyKeyboard);    
                        
                }
                
            }
            else if (update.Type == UpdateType.CallbackQuery)
            {
                var callbackQuery = update.CallbackQuery;
            }
        }


        static async Task HandleReceivedDaysAsync(Message message2)
        {
            var daysChatId = message2.Chat.Id;

            if (message2.Text != null)
            {
                
                if (double.TryParse(message2.Text, out double inputNumber3))
                {
                    var not = inputNumber3 * 150;
                    var replyKeyboard = new ReplyKeyboardMarkup(
                        new List<KeyboardButton[]>()
                        {
                            new KeyboardButton[]
                            {
                                new(calcs),
                                new(showPromos)
                            }
                        })
                    {
                        ResizeKeyboard = true
                    };
                    await bot.SendTextMessageAsync(daysChatId, $"Колличество нефрита через {inputNumber3} дней с пропуском снабжения - {not}",
                        replyMarkup: replyKeyboard);
                    await removeChatFromStage(daysChatId);
                }
                else
                {
                    await bot.SendTextMessageAsync(daysChatId, "Пожалуйста, введите число в правильном формате.");
                }
            }
        }

        private static async Task HandleReceivedNumbersAsync(Message message)
        {
            var NumbersChatId = message.Chat.Id;

            if (message.Text != null)
            {
                if (double.TryParse(message.Text, out double inputNumber))
                {
                    var result1 = (int)Math.Floor(inputNumber / 160);
                    double result2 = inputNumber - (result1 * 160);
                    var replyKeyboard = new ReplyKeyboardMarkup(
                        new List<KeyboardButton[]>()
                        {
                            new KeyboardButton[]
                            {
                                new(calcs),
                                new(showPromos)
                            }
                        })
                    {
                        ResizeKeyboard = true
                    };
                    await bot.SendTextMessageAsync(NumbersChatId, $"Колличество прыжков: {result1} + {result2} нефрита остаток",
                        replyMarkup: replyKeyboard);
                    await removeChatFromStage(NumbersChatId);
                }
                else
                {
                    await bot.SendTextMessageAsync(NumbersChatId, "Пожалуйста, введите число в правильном формате.");
                }
            }
        }
        
        static async Task HandleReceivedDaysAsyncFTP(Message message1)
        {
            var daysChatId = message1.Chat.Id;

            if (message1.Text != null)
            {
                if (double.TryParse(message1.Text, out double inputNumber2))
                {
                    var f2p = inputNumber2 * 60;
                    var replyKeyboard = new ReplyKeyboardMarkup(
                        new List<KeyboardButton[]>()
                        {
                            new KeyboardButton[]
                            {
                                new(calcs),
                                new(showPromos)
                            }
                        })
                    {
                        ResizeKeyboard = true
                    };
                    await bot.SendTextMessageAsync(daysChatId, $"Колличество нефрита через {inputNumber2} дней c дейликов - {f2p}",
                        replyMarkup: replyKeyboard);
                    await removeChatFromStage(daysChatId);
                }
                else
                {
                    await bot.SendTextMessageAsync(daysChatId, "Пожалуйста, введите число в правильном формате.");
                }
            }
        }

        static async Task PromoActivation(long chatId, Message message)
        {
            await removeChatFromStage(chatId);
        }

        static async Task AddPromocodes(long chatId, Message message)
        {
            var promo = message.Text;
            promos.Add(promo);
        }
        

        private static async Task setChatInStage(long chatId, string stageName)
        {
            stages.Add(new(chatId, stageName));
        }

        private static async Task<string> getChatStage(long chatId)
        {
            return stages.FirstOrDefault(x => x.Item1 == chatId).Item2;
        }

        private static async Task removeChatFromStage(long chatId)
        {
            stages.RemoveAll(x => x.Item1 == chatId);
        }
        

        private static async Task SendQuestion1(long chatId, Update update)
        {
            Answers.Remove(chatId);
            var replyMarkup = new ReplyKeyboardMarkup(new[]
            {
                new KeyboardButton("Есть пропуск снабжения"),
                new KeyboardButton("Нет пропуска снабжения")
            })
            {
                ResizeKeyboard = true
            };
            await bot.SendTextMessageAsync(chatId, "Есть ли у вас пропуск снабжения?", replyMarkup: replyMarkup);
            await removeChatFromStage(chatId);
        }

        private static async Task SendQuestion2(long chatId, Update update)
        {
            var replyMarkup = new ReplyKeyboardMarkup(new[]
            {
                new KeyboardButton("Есть боевой пропуск"),
                new KeyboardButton("Нет боевого пропуска")
            })
            {
                ResizeKeyboard = true
            };
            await bot.SendTextMessageAsync(chatId, "Есть ли у вас боевой пропуск?", replyMarkup: replyMarkup);
            await removeChatFromStage(chatId);
        }

        private static async Task SendQuestion3(long chatId, Update update)
        {
            var replyMarkup = new ReplyKeyboardMarkup(new[]
            {
                new KeyboardButton("I"),
                new KeyboardButton("II"),
                new KeyboardButton("III"),
                new KeyboardButton("IV"),
                new KeyboardButton("V"),
                new KeyboardButton("VI"),
                new KeyboardButton("VII"),
                new KeyboardButton("VIII"),
                new KeyboardButton("IX"),
                new KeyboardButton("X"),
                new KeyboardButton("XI"),
                new KeyboardButton("XII")
            })
            
            {
                ResizeKeyboard = true
            };
            
            await bot.SendTextMessageAsync(chatId, "Сколько этажей воспоминания хаоса вы проходите?", replyMarkup: replyMarkup);
            await removeChatFromStage(chatId);
            
        }

        private static async Task SendQuestion4(long chatId, Update update)
        {
            var replyMarkup = new ReplyKeyboardMarkup(new[]
            {
                new KeyboardButton("1"),
                new KeyboardButton("2"),
                new KeyboardButton("3"),
                new KeyboardButton("4")
            })
            {
                ResizeKeyboard = true
            };
            await bot.SendTextMessageAsync(chatId, "Сколько этажей чистого вымысла вы проходите?",
                replyMarkup: replyMarkup);
            await removeChatFromStage(chatId);
        }
        
        private static async Task ProcessAnswer1(long chatId, Update update)
        {
            var answerText = update.Message.Text;
            var answer1 = answerText;
            Answers[chatId].Add(answer1);
            await removeChatFromStage(chatId);
            await bot.SendTextMessageAsync(chatId, $"Вы ответили на первый вопрос: {answerText}");
        }

        private static async Task ProcessAnswer2(long chatId, Update update)
        {
            var answerText = update.Message.Text;
            var answer2 = answerText;
            Answers[chatId].Add(answer2);
            await removeChatFromStage(chatId);
            await bot.SendTextMessageAsync(chatId, $"Вы ответили на второй вопрос: {answerText}");
        }

        private static async Task ProcessAnswer3(long chatId, Update update)
        {
            var answerText = update.Message.Text;
            var answer3 = answerText;
            Answers[chatId].Add(answer3);
            await removeChatFromStage(chatId);
            await bot.SendTextMessageAsync(chatId, $"Вы ответили на третий вопрос: {answerText}");
        }

        private static async Task ProcessAnswer4(long chatId, Update update)
        {
            var answerText = update.Message.Text;
            var answer4 = answerText;
            Answers[chatId].Add(answer4);
            await removeChatFromStage(chatId);
            await bot.SendTextMessageAsync(chatId, $"Вы ответили на четвертый вопрос: {answerText}");
        }
        
        
        static async Task TotalJadePatch(long chatId, Message message1)
        {
            var daysChatId = message1.Chat.Id;
            
            List<string> allStrings = Answers.Values.SelectMany(list => list).ToList();

            int totalJade = 0;
            int totalJade2 = 0;
            var daysInPatch = 49;
            var bpEventWarps = 0;
            var totalEventWarps = 0;
            var bpStandartWarps = 0;
            var totalStandartWarps = 0;

            foreach (var answer in allStrings)
            {
                var result1 = 0;
                
                if (answer == "Есть пропуск снабжения")
                {
                    result1 = daysInPatch * (60 + 90);
                }
                else if(answer == "Нет пропуска снабжения")
                {
                    result1 = daysInPatch * 60;
                }
                
                
                var result2 = 0;
                var warpsEvent = 0;
                var warpsStandart = 0;
                
                if (answer == "Есть боевой пропуск")
                {
                    result2 = 680;
                    warpsEvent = 4;
                    warpsStandart = 5;
                }
                else if(answer == "Нет боевого пропуска")
                {
                    result2 = 0;
                }

                
                var result3 = 0;
                
                if (answer == "I")
                {
                    result3 = 60;
                }
                else if (answer == "II")
                {
                    result3 = 120;
                }
                else if (answer == "III")
                {
                    result3 = 180;
                }
                else if (answer == "IV")
                {
                    result3 = 240;
                }
                else if (answer == "V")
                {
                    result3 = 300;
                }
                else if (answer == "VI")
                {
                    result3 = 360;
                }
                else if (answer == "VII")
                {
                    result3 = 420;
                }
                else if (answer == "VIII")
                {
                    result3 = 480;
                }
                else if (answer == "II")
                {
                    result3 = 540;
                }
                else if (answer == "X")
                {
                    result3 = 600;
                }
                else if (answer == "XI")
                {
                    result3 = 660;
                }
                else if (answer == "XII")
                {
                    result3 = 720;
                }

                
                var result4 = 0;
                
                if (answer == "1")
                {
                    result4 = 60;
                }
                else if (answer == "2")
                {
                    result4 = 120;
                }
                else if (answer == "3")
                {
                    result4 = 180;
                }
                else if (answer == "4")
                {
                    result4 = 240;
                }

                //Jade
                var firstEvent = 2400;
                var secondEvent = 600;
                var thirdEvent = 500;
                var fourthEvent = 500;
                var fifthEvent = 2160;
                var compensation = 600;
                var codes = 300;
                var newMissions = 800;
                var companionMissions = 100;
                var locationExploring = 2000;
                var simulatedJade = 1575;
                
                //Warps
                
                //Event
                var loginWarps = 10;
                var randomGift = 10;
                var shopWarps = 5;
                
                //Standart
                var shopWarps2 = 5;
                var simulatedWarps = 7;




                totalJade += result4 + result3 + result2 + result1; 
                totalJade2 = firstEvent + secondEvent + thirdEvent + fourthEvent + fifthEvent + compensation + codes + newMissions + companionMissions + locationExploring + simulatedJade + totalJade;

                bpEventWarps += warpsEvent;
                bpStandartWarps += warpsStandart;
                
                totalEventWarps = bpEventWarps + loginWarps + randomGift + shopWarps;

                totalStandartWarps = bpStandartWarps + shopWarps2 + simulatedWarps;
                
            }
            
            var warpsCounter = (int)Math.Floor((double)(totalJade2 / 160));
            var totalEventWarps2 = totalEventWarps  + warpsCounter;

            var replyKeyboard = new ReplyKeyboardMarkup(
                new List<KeyboardButton[]>()
                {
                    new KeyboardButton[]
                    {
                        new(calcs),
                        new(showPromos)
                    },
                })
            {
                ResizeKeyboard = true
            };
            await bot.SendTextMessageAsync(chatId, $"Total jade~ {totalJade2} ({warpsCounter} Warps), Event Warps {totalEventWarps}, Standart Warps {totalStandartWarps}, Total Event Warps {totalEventWarps2}",
                replyMarkup: replyKeyboard);
        }
        
        public static void Main(string[] args) 
            { 
                Console.WriteLine("Let`s go " + bot.GetMeAsync().Result.FirstName);
            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
                {
                    AllowedUpdates = {  }, 
                };

            bot.StartReceiving(
                (client, update, cancellationToken) => HandleUpdateAsync(client, update, cancellationToken),
                (client, exception, arg3) => HandleErrorAsync(client, exception, arg3),
                receiverOptions,
                cancellationToken
                              );

            Console.ReadLine();
        }

        private static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }
    }
}