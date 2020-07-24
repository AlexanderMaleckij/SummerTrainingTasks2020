using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClientSide.Tests
{
    [TestClass()]
    public class TranscoderTests
    {
        [TestMethod()]
        public void TranscodeTest()
        {
            string ruStr = "Мороз и солнце; день чудесный!\n" +
                           "Еще ты дремлешь, друг прелестный —\n" +
                           "Пора, красавица, проснись:\n" +
                           "Открой сомкнуты негой взоры";

            string expectedTranscodedRuStr = "Moroz i solntse; den' chudesnyy!\n" +
                                             "Esche ty dremlesh', drug prelestnyy —\n" +
                                             "Pora, krasavitsa, prosnis':\n" +
                                             "Otkroy somknuty negoy vzory";

            string enStr = "Let America be America again.\n" +
                           "Let it be the dream it used to be.\n" +
                           "Let it be the pioneer on the plain\n" +
                           "Seeking a home where he himself is free.";

            string expectedTranscodedEnStr = "Лет Америка бе Америка агаин.\n" +
                                             "Лет ит бе тхе дреам ит усед то бе.\n" +
                                             "Лет ит бе тхе пионеер он тхе плаин\n" +
                                             "Сеекинг а хоме вхере хе химселф ис фрее.";

            Assert.AreEqual(Transcoder.Transcode(ruStr), expectedTranscodedRuStr);
            Assert.AreEqual(Transcoder.Transcode(enStr), expectedTranscodedEnStr);
        }
    }
}