using WMPLib;
using System.Media;

namespace Prototype
{
    public class GameMusic
    {
        private WindowsMediaPlayer m_WMP = new WindowsMediaPlayer();
        private EMusic m_Music = EMusic.None;

        public void PlayMusic(EMusic a_Music)
        {
            if (m_Music == a_Music)
                return;
            m_Music = a_Music;
            string fileName = "";
            switch (m_Music)
            {
                case EMusic.Town: fileName = "Old RuneScape Soundtrack - Spirit.mp3"; break;
                case EMusic.Market: fileName = "Everquest - Shopping Merchant (HQ).mp3"; break;
            }
            string path = Path.Combine(Environment.CurrentDirectory, @"Data\Musics\", fileName);
            m_WMP.URL = path;
            m_WMP.settings.setMode("loop", true);
            m_WMP.controls.play();
        }
    }
}