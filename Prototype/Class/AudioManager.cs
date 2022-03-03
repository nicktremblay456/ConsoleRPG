using WMPLib;

namespace Prototype
{
    /// <summary>
    /// Singleton Class
    /// </summary>
    public sealed class AudioManager
    {
        private static AudioManager? m_Instance = null;
        private static readonly object m_Lock = new object();
        public static AudioManager? Instance 
        { 
            get
            {
                lock (m_Lock)
                {
                    if (m_Instance == null)
                        m_Instance = new AudioManager();

                    return m_Instance;
                }
            }
        }

        private WindowsMediaPlayer m_MusicWMP = new WindowsMediaPlayer();
        private EMusic m_Music = EMusic.None;
        private int m_MusicVolume = 25;
        private int m_SfxVolume = 50;

        private AudioManager()
        {
            m_Instance = this;
            m_MusicWMP.settings.setMode("loop", true);
        }

        public void PlayMusic(EMusic a_Music)
        {
            if (m_Music == a_Music)
                return;
            m_Music = a_Music;
            string fileName = "";
            switch (m_Music)
            {
                case EMusic.Main: fileName = "Freeport_Docks_Everquest.mp3"; break;
                case EMusic.Town: fileName = "EverQuest_Music_POP_POK.mp3"; break;
                case EMusic.Market: fileName = "Everquest_Shopping_Merchant.mp3"; break;
                case EMusic.Combat: fileName = "EverQuest_Music_Planes_of_Power_Battle_Music_1.mp3"; break;
            }
            if (m_Music == EMusic.Market)
                m_MusicVolume = 100;
            else
                m_MusicVolume = 25;

            m_MusicWMP.settings.volume = m_MusicVolume;
            string path = Path.Combine(Environment.CurrentDirectory, @"Data\Musics\", fileName);
            m_MusicWMP.URL = path;
            try
            {
                m_MusicWMP.controls.play();
            }
            catch { }
        }

        public void PlaySoundEffect(ESoundEffect a_SoundEffect)
        {
            WindowsMediaPlayer sfxPlayer = new WindowsMediaPlayer();
            sfxPlayer.settings.volume = m_SfxVolume;
            string fileName = "";

            switch(a_SoundEffect)
            {
                case ESoundEffect.Select: fileName = "button_1.mp3"; break;
                case ESoundEffect.Item: fileName = "itemclth.mp3"; break;
                case ESoundEffect.BuyItem: fileName = "buyitem.mp3"; break;
                case ESoundEffect.Drink: fileName = "drink.mp3"; break;
                case ESoundEffect.Door: fileName = "doorst_c.mp3"; break;
                case ESoundEffect.Swing: fileName = "swingbig.mp3"; break;
                case ESoundEffect.ArrowHit: fileName = "arrowhit.mp3"; break;
                case ESoundEffect.SpellDamage: fileName = "spell_damage.mp3"; break;
                case ESoundEffect.SpellBuff: fileName = "spell_buff.mp3"; break;
                case ESoundEffect.SpellHeal: fileName = "spell_heal.mp3"; break;
                case ESoundEffect.GetHit: fileName = "gethit2mb.mp3"; break;
                case ESoundEffect.OpenChest: fileName = "chest_cl.mp3"; break;
                case ESoundEffect.BookPage: fileName = "page_turn01.mp3"; break;
                case ESoundEffect.LevelUp: fileName = "levelup.mp3"; break;
            }
            if (fileName != string.Empty)
            {
                string path = Path.Combine(Environment.CurrentDirectory, @"Data\SoundEffects\", fileName);
                sfxPlayer.URL = path;
                sfxPlayer.controls.play();
            }
        }
    }
}