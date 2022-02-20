using WMPLib;

namespace Prototype
{
    public class AudioManager
    {
        private WindowsMediaPlayer m_MusicWMP = new WindowsMediaPlayer();
        private EMusic m_Music = EMusic.None;

        public AudioManager()
        {
            m_MusicWMP.settings.setMode("loop", true);
            m_MusicWMP.settings.volume /= 2;
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
                case EMusic.Town: fileName = "Everquest_Qeynos_hills_Cottage.mp3"; break;
                case EMusic.Market: fileName = "Everquest_Shopping_Merchant.mp3"; break;
            }
            string path = Path.Combine(Environment.CurrentDirectory, @"Data\Musics\", fileName);
            m_MusicWMP.URL = path;
            m_MusicWMP.controls.play();
        }

        public static void PlaySoundEffect(ESoundEffect a_SoundEffect)
        {
            WindowsMediaPlayer sfxPlayer = new WindowsMediaPlayer();
            string fileName = "";

            switch(a_SoundEffect)
            {
                case ESoundEffect.Select: fileName = "button_1.mp3"; break;
                case ESoundEffect.Item: fileName = "itemclth.mp3"; break;
                case ESoundEffect.BuyItem: fileName = "buyitem.mp3"; break;
                case ESoundEffect.Drink: fileName = "drink.mp3"; break;
                case ESoundEffect.Door: fileName = "doorst_c.mp3"; break;
                case ESoundEffect.ArrowHit: fileName = "arrowhit.mp3"; break;
                case ESoundEffect.SpellDamage: fileName = "spell_damage.mp3"; break;
                case ESoundEffect.SpellBuff: fileName = "spell_buff.mp3"; break;
                case ESoundEffect.SpellHeal: fileName = "spell_heal.mp3"; break;
                case ESoundEffect.GetHit: fileName = "gethit2mb.mp3"; break;
                case ESoundEffect.OpenChest: fileName = "chest_cl.mp3"; break;
            }
            string path = Path.Combine(Environment.CurrentDirectory, @"Data\SoundEffects\", fileName);
            sfxPlayer.URL = path;
            sfxPlayer.controls.play();
        }
    }
}