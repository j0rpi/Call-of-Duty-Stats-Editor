/*
    =========================
        COD Stats Editor
           by j0rpi
      http://www.j0rpi.net
    =========================
  
     Did anyone say Ghosts?
 
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;
using INI;


namespace MW3_Stats_Editor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        #region Basic Stuff
        [DllImport("kernel32.dll")]
        private static extern Int32 ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [In, Out] byte[] buffer, UInt32 size, out IntPtr lpNumberOfBytesWritten);
        [DllImport("kernel32.dll")]
        private static extern Int32 WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [In, Out] byte[] buffer, UInt32 size, out IntPtr lpNumberOfBytesWritten);
        IntPtr pHandel;
        public bool Process_Handle(string ProcessName)
        {
            try
            {
                Process[] ProcList = Process.GetProcessesByName(ProcessName);
                if (ProcList.Length == 0)
                    return false;
                else
                {
                    pHandel = ProcList[0].Handle;
                    return true;
                }
            }
            catch (Exception ex)
            { Console.Beep(); Console.WriteLine("Process_Handle - " + ex.Message); return false; }
        }
        private byte[] Read(int Address, int Length)
        {
            byte[] Buffer = new byte[Length];
            IntPtr Zero = IntPtr.Zero;
            ReadProcessMemory(pHandel, (IntPtr)Address, Buffer, (UInt32)Buffer.Length, out Zero);
            return Buffer;
        }
        private void Write(int Address, int Value)
        {
            byte[] Buffer = BitConverter.GetBytes(Value);
            IntPtr Zero = IntPtr.Zero;
            WriteProcessMemory(pHandel, (IntPtr)Address, Buffer, (UInt32)Buffer.Length, out Zero);
        }
        #endregion
        #region Write Functions (Integer & String)
        public void WriteInteger(int Address, int Value)
        {
            Write(Address, Value);
        }
        public void WriteString(int Address, string Text)
        {
            byte[] Buffer = new ASCIIEncoding().GetBytes(Text);
            IntPtr Zero = IntPtr.Zero;
            WriteProcessMemory(pHandel, (IntPtr)Address, Buffer, (UInt32)Buffer.Length, out Zero);
        }
        public void WriteBytes(int Address, byte[] Bytes)
        {
            IntPtr Zero = IntPtr.Zero;
            WriteProcessMemory(pHandel, (IntPtr)Address, Bytes, (uint)Bytes.Length, out Zero);
        }
        public void WriteNOP(int Address)
        {
            byte[] Buffer = new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90 };
            IntPtr Zero = IntPtr.Zero;
            WriteProcessMemory(pHandel, (IntPtr)Address, Buffer, (UInt32)Buffer.Length, out Zero);
        }


        #endregion
        #region Read Functions (Integer & String)
        public int ReadInteger(int Address, int Length = 4)
        {
            return BitConverter.ToInt32(Read(Address, Length), 0);
        }
        public string ReadString(int Address, int Length = 4)
        {
            return new ASCIIEncoding().GetString(Read(Address, Length));
        }
        public byte[] ReadBytes(int Address, int Length)
        {
            return Read(Address, Length);
        }
        #endregion
        #region Adresses and other stuff /j0rpi

        // Adresses found by j0rpi

        // STATS RELATED ADRESSES
        int SCORE = 0x321E8934;
        int WINS = 0x321E8F40;
        int LOSSES = 0x321E9748;
        int KILLS = 0x321E8B3C;
        int DEATHS = 0x321EAB5C;
        int ASSISTS = 0x321E9B4C;
        int KILLSTREAK = 0x321EA354;
        int HEADSHOTS = 0x321E9344;
        int EXP = 0x01DC02B8;
        int NAME = 0xD881D7E;
        int TIME = 0x1DC0518;
        int DOUBLEWEAPONXP = 0x1DC2385;
        int DOUBLEXP = 0x1DC237D;
        int CLASSES = 0x1DC232F;
        int PRESTIGE = 0x1DC04C8;
        int TOKENS = 0x1DC2327;
        int PROPERKS = 0x1DC1252;
        byte[] LOCKPERKS;
        byte[] UNLOCKPERKS;
        byte[] UNLOCKTITLES;
        byte[] UNLOCKEMBLEMS;
        byte[] UNLOCKCHALLENGES;
        byte[] RESETCHALLENGES;
        byte[] RESETTITLES;
        byte[] RESETEMBLEMS;
        byte[] TEST;
        
        // WEAPON ADRESSES
        int M4A1 = 0x1db9fe0;
        int M16A4 = 0x1db9fe8;
        int SCAR = 0x1db9ff4;
        int CM901 = 0x1db9ffc;
        int TYPE95 = 0x1db9fdc;
        int G36C = 0x1db9ff0;
        int ACR = 0x1db9fd8;
        int MK14 = 0x1db9fec;
        int AK47 = 0x1db9fe4;
        int FAD = 0x1db9ff8;
        int MP5 = 0x1dba000;
        int UMP = 0x1dba010;
        int PP90 = 0x1dba00c;
        int P90 = 0x1dba008;
        int PM9 = 0x1dba004;
        int MP7 = 0x1dba014;
        int L86 = 0x1dba050;
        int MG36 = 0x1dba054;
        int PKP = 0x1dba04c;
        int MK46 = 0x1dba048;
        int M60 = 0x1dba044;
        int BARRET = 0x1dba058;
        int AWP = 0x1dba06c;
        int DRAGUNOV = 0x1dba064;
        int AS50 = 0x1dba068;
        int RSASS = 0x1dba060;
        int MSR = 0x1dba05c;
        int USAS12 = 0x1dba03c;
        int KSG12 = 0x1dba040;
        int SPAS12 = 0x1dba02c;
        int AA12 = 0x1dba030;
        int STRIKER = 0x1dba034;
        int M1887 = 0x1dba038;
        int FMG9 = 0x1dba018;
        int MP9 = 0x1dba020;
        int SKORPION = 0x1dba024;
        int G18 = 0x1dba01c;
        int USP = 0x1db9fc0;
        int P99 = 0x1db9fd0;
        int MP412 = 0x1db9fc4;
        int MAGNUM = 0x1db9fc8;
        int FIVESEVEN = 0x1db9fd4;
        int DEAGLE = 0x1db9fcc;
        int SMAW = 0x1dba098;
        int JAVELIN = 0x1dba090;
        int XM25 = 0x1dba0c4;
        int M320 = 0x1dba09c;
        int RPG = 0x1dba074;
        int SHIELD = 0x1dba0b8;

        // Titles, Emblems, Challenges
        int challenges = 0x1DC1045;
        int titles = 0x1DC216C;
        int emblems = 0x1DC20EC;

        // Offset (Some adresses needs an offset...)
        int offset = 0x6300;

        // DEFINE APP DIR
        string appPath = Path.GetDirectoryName(Application.ExecutablePath);

        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {
            // Parent label with picturebox to create transparency
            label10.Parent = pictureBox1;
            linkLabel1.Parent = pictureBox1;

            // Set folderBrowserDialog properties
            openFileDialog1.Title = "Select MW3 Stats Editor Backup File";
            openFileDialog1.FileName = "MW3StatsBackup_X.INI";

            // Shitty Scrolling Effect. (lol)
            timer2.Enabled = true;
            timer2.Interval = 10;
            timer3.Interval = 10;
            timer3.Enabled = true;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Process_Handle("iw5mp") == true)
            {
                // Read Score
                textBox1.Text = Convert.ToString(ReadInteger(EXP));

                // Read Wins
                textBox2.Text = Convert.ToString(ReadInteger(WINS));

                // Read Losses
                textBox3.Text = Convert.ToString(ReadInteger(LOSSES));

                // Read Kills
                textBox4.Text = Convert.ToString(ReadInteger(KILLS));

                // Read Deaths
                textBox5.Text = Convert.ToString(ReadInteger(DEATHS));

                // Read Assists
                textBox6.Text = Convert.ToString(ReadInteger(ASSISTS));

                // Read Killstreak
                textBox7.Text = Convert.ToString(ReadInteger(KILLSTREAK));

                // Read Headshots
                textBox8.Text = Convert.ToString(ReadInteger(HEADSHOTS));

                // Read EXP
                textBox9.Text = Convert.ToString(ReadInteger(SCORE));

                // Read Time
                textBox10.Text = Convert.ToString(ReadInteger(TIME));

                // Read Double Weapon XP
                textBox11.Text = Convert.ToString(ReadInteger(DOUBLEWEAPONXP));

                // Read Double XP
                textBox12.Text = Convert.ToString(ReadInteger(DOUBLEXP));

                // Read Classes
                comboBox1.Text = Convert.ToString(ReadInteger(CLASSES));

                // Read Prestige
                comboBox2.Text = Convert.ToString(ReadInteger(PRESTIGE));

                // Read Prestige Tokens
                textBox13.Text = Convert.ToString(ReadInteger(TOKENS));

                WriteInteger(Convert.ToInt16(0x14432DBD0), 31);
            }
            else
            {
                MessageBox.Show(null, "Could not read stats. Make sure MW3 is running.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Define Backup File - It'll be called MW3Backup_Year_Month_Day_Secs.ini
            IniFile Backup = new IniFile(appPath + @"\MW3StatsBackup_" + DateTime.Now.ToString().Replace(":", "_") + ".ini");

            // Use values in each textbox and write to new ini file
            Backup.IniWriteValue("MW3Stats", "XP", textBox1.Text.ToString());
            Backup.IniWriteValue("MW3Stats", "WINS", textBox2.Text.ToString());
            Backup.IniWriteValue("MW3Stats", "LOSSES", textBox3.Text.ToString());
            Backup.IniWriteValue("MW3Stats", "KILLS", textBox4.Text.ToString());
            Backup.IniWriteValue("MW3Stats", "DEATHS", textBox5.Text.ToString());
            Backup.IniWriteValue("MW3Stats", "ASSISTS", textBox6.Text.ToString());
            Backup.IniWriteValue("MW3Stats", "KILLSTREAK", textBox7.Text.ToString());
            Backup.IniWriteValue("MW3Stats", "HEADSHOTS", textBox8.Text.ToString());
            Backup.IniWriteValue("MW3Stats", "SCORE", textBox9.Text.ToString());
            Backup.IniWriteValue("MW3Stats", "TIME", textBox10.Text.ToString());
            Backup.IniWriteValue("MW3Stats", "DOUBLEWEAPONXP", textBox11.Text.ToString());
            Backup.IniWriteValue("MW3Stats", "DOUBLEXP", textBox12.Text.ToString());
            Backup.IniWriteValue("MW3Stats", "CLASSES", comboBox1.Text.ToString());
            Backup.IniWriteValue("MW3Stats", "PRESTIGE", comboBox2.Text.ToString());
            Backup.IniWriteValue("MW3Stats", "TOKENS", textBox13.Text.ToString());
            MessageBox.Show("Backup Successfully Saved To " + Backup.path);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Process_Handle("iw5mp") == true)
            {
                // Write Score
                WriteInteger(SCORE, int.Parse(textBox9.Text));

                // Write Wins
                WriteInteger(WINS, int.Parse(textBox2.Text));

                // Write Losses
                WriteInteger(LOSSES, int.Parse(textBox3.Text));

                // Write Kills
                WriteInteger(KILLS, int.Parse(textBox4.Text));

                // Write Deaths
                WriteInteger(DEATHS, int.Parse(textBox5.Text));

                // Write Assists
                WriteInteger(ASSISTS, int.Parse(textBox6.Text));

                // Write Killstreak
                WriteInteger(KILLSTREAK, int.Parse(textBox7.Text));

                // Write Headshots
                WriteInteger(HEADSHOTS, int.Parse(textBox8.Text));

                // Write EXP
                WriteInteger(EXP, int.Parse(textBox1.Text));

                // Write Time
                WriteInteger(TIME, int.Parse(textBox10.Text));

                // Write Double Weapon XP
                WriteInteger(DOUBLEWEAPONXP, int.Parse(textBox11.Text));

                // Write Double XP
                WriteInteger(DOUBLEXP, int.Parse(textBox12.Text));

                // Write Classes
                WriteInteger(CLASSES, int.Parse(comboBox1.Text));

                // Write Prestige
                WriteInteger(PRESTIGE, int.Parse(comboBox2.Text));

                // Write Prestige Tokens
                WriteInteger(TOKENS, int.Parse(textBox13.Text));
             

                // Success
                MessageBox.Show(null, "Stats has been saved. Play some games to make sure changes stay.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show(null, "Could not save stats. Make sure MW3 is running.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                // Define our selected stats file
                IniFile backupfile = new IniFile(openFileDialog1.FileName);
                

                // Now read data from ini to textboxes
                textBox1.Text = backupfile.IniReadValue("MW3Stats", "XP");
                textBox2.Text = backupfile.IniReadValue("MW3Stats", "WINS");
                textBox3.Text = backupfile.IniReadValue("MW3Stats", "LOSSES");
                textBox4.Text = backupfile.IniReadValue("MW3Stats", "KILLS");
                textBox5.Text = backupfile.IniReadValue("MW3Stats", "DEATHS");
                textBox6.Text = backupfile.IniReadValue("MW3Stats", "ASSISTS");
                textBox7.Text = backupfile.IniReadValue("MW3Stats", "KILLSTREAK");
                textBox8.Text = backupfile.IniReadValue("MW3Stats", "HEADSHOTS");
                textBox9.Text = backupfile.IniReadValue("MW3Stats", "SCORE");
                textBox10.Text = backupfile.IniReadValue("MW3Stats", "TIME");
                textBox11.Text = backupfile.IniReadValue("MW3Stats", "DOUBLEWEAPONXP");
                textBox12.Text = backupfile.IniReadValue("MW3Stats", "DOUBLEXP");
                comboBox1.Text = backupfile.IniReadValue("MW3Stats", "CLASSES");
                comboBox2.Text = backupfile.IniReadValue("MW3Stats", "PRESTIGE");
                textBox13.Text = backupfile.IniReadValue("MW3Stats", "TOKENS");

                // Tell our user that it succeeded
                MessageBox.Show(null, "Load Successful!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

            if (Process_Handle("iw5mp"))
            {

                // There's 15 perks total. We will therfor make a 15 byte array. 0 = Lock / 7 = Unlock
                UNLOCKPERKS = new byte[15] { 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7 };
                WriteBytes(PROPERKS, UNLOCKPERKS);
                MessageBox.Show(null, "Perks has been set to PRO.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(null, "Could not unlock perks. Make sure MW3 is running.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (Process_Handle("iw5mp"))
            {
                // There's 15 perks total. We will therfor make a 15 byte array. 0 = Lock / 7 = Unlock
                LOCKPERKS = new byte[15] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                WriteBytes(PROPERKS, LOCKPERKS);
                MessageBox.Show(null, "Perks has been reset.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(null, "Could not lock perks. Make sure MW3 is running.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {



            if (Process_Handle("iw5mp"))
            {
                // Set weapons to max level 
                WriteBytes(M4A1 + offset, BitConverter.GetBytes(9999999));
                WriteBytes(M16A4 + offset, BitConverter.GetBytes(9999999));
                WriteBytes(SCAR + offset, BitConverter.GetBytes(9999999));
                WriteBytes(CM901 + offset, BitConverter.GetBytes(9999999));
                WriteBytes(TYPE95 + offset, BitConverter.GetBytes(9999999));
                WriteBytes(G36C + offset, BitConverter.GetBytes(9999999));
                WriteBytes(ACR + offset, BitConverter.GetBytes(9999999));
                WriteBytes(MK14 + offset, BitConverter.GetBytes(9999999));
                WriteBytes(AK47 + offset, BitConverter.GetBytes(9999999));
                WriteBytes(FAD + offset, BitConverter.GetBytes(9999999));
                WriteBytes(MP5 + offset, BitConverter.GetBytes(9999999));
                WriteBytes(UMP + offset, BitConverter.GetBytes(9999999));
                WriteBytes(PP90 + offset, BitConverter.GetBytes(9999999));
                WriteBytes(P90 + offset, BitConverter.GetBytes(9999999));
                WriteBytes(PM9 + offset, BitConverter.GetBytes(9999999));
                WriteBytes(MP7 + offset, BitConverter.GetBytes(9999999));
                WriteBytes(L86 + offset, BitConverter.GetBytes(9999999));
                WriteBytes(MG36 + offset, BitConverter.GetBytes(9999999));
                WriteBytes(PKP + offset, BitConverter.GetBytes(9999999));
                WriteBytes(MK46 + offset, BitConverter.GetBytes(9999999));
                WriteBytes(M60 + offset, BitConverter.GetBytes(9999999));
                WriteBytes(BARRET + offset, BitConverter.GetBytes(9999999));
                WriteBytes(AWP + offset, BitConverter.GetBytes(9999999));
                WriteBytes(DRAGUNOV + offset, BitConverter.GetBytes(9999999));
                WriteBytes(AS50 + offset, BitConverter.GetBytes(9999999));
                WriteBytes(RSASS + offset, BitConverter.GetBytes(9999999));
                WriteBytes(MSR + offset, BitConverter.GetBytes(9999999));
                WriteBytes(USAS12 + offset, BitConverter.GetBytes(9999999));
                WriteBytes(KSG12 + offset, BitConverter.GetBytes(9999999));
                WriteBytes(SPAS12 + offset, BitConverter.GetBytes(9999999));
                WriteBytes(AA12 + offset, BitConverter.GetBytes(9999999));
                WriteBytes(STRIKER + offset, BitConverter.GetBytes(9999999));
                WriteBytes(M1887 + offset, BitConverter.GetBytes(9999999));
                WriteBytes(FMG9 + offset, BitConverter.GetBytes(9999999));
                WriteBytes(SKORPION + offset, BitConverter.GetBytes(9999999));
                WriteBytes(G18 + offset, BitConverter.GetBytes(9999999));
                WriteBytes(USP + offset, BitConverter.GetBytes(9999999));
                WriteBytes(P99 + offset, BitConverter.GetBytes(9999999));
                WriteBytes(MP412 + offset, BitConverter.GetBytes(9999999));
                WriteBytes(MAGNUM + offset, BitConverter.GetBytes(9999999));
                WriteBytes(FIVESEVEN + offset, BitConverter.GetBytes(9999999));
                WriteBytes(DEAGLE + offset, BitConverter.GetBytes(9999999));
                WriteBytes(SMAW + offset, BitConverter.GetBytes(9999999));
                WriteBytes(JAVELIN + offset, BitConverter.GetBytes(9999999));
                WriteBytes(XM25 + offset, BitConverter.GetBytes(9999999));
                WriteBytes(M320 + offset, BitConverter.GetBytes(9999999));
                WriteBytes(RPG + offset, BitConverter.GetBytes(9999999));
                WriteBytes(SHIELD + offset, BitConverter.GetBytes(9999999));
                MessageBox.Show(null, "Weapons has been unlocked. Get a kill and finish a game for changes to take effect.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(null, "Could not unlock weapons. Make sure MW3 is running.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (Process_Handle("iw5mp"))
            {
                WriteBytes(M4A1 + offset, BitConverter.GetBytes(1));
                WriteBytes(M16A4 + offset, BitConverter.GetBytes(1));
                WriteBytes(SCAR + offset, BitConverter.GetBytes(1));
                WriteBytes(CM901 + offset, BitConverter.GetBytes(1));
                WriteBytes(TYPE95 + offset, BitConverter.GetBytes(1));
                WriteBytes(G36C + offset, BitConverter.GetBytes(1));
                WriteBytes(ACR + offset, BitConverter.GetBytes(1));
                WriteBytes(MK14 + offset, BitConverter.GetBytes(1));
                WriteBytes(AK47 + offset, BitConverter.GetBytes(1));
                WriteBytes(FAD + offset, BitConverter.GetBytes(1));
                WriteBytes(MP5 + offset, BitConverter.GetBytes(1));
                WriteBytes(UMP + offset, BitConverter.GetBytes(1));
                WriteBytes(PP90 + offset, BitConverter.GetBytes(1));
                WriteBytes(P90 + offset, BitConverter.GetBytes(1));
                WriteBytes(PM9 + offset, BitConverter.GetBytes(1));
                WriteBytes(MP7 + offset, BitConverter.GetBytes(1));
                WriteBytes(L86 + offset, BitConverter.GetBytes(1));
                WriteBytes(MG36 + offset, BitConverter.GetBytes(1));
                WriteBytes(PKP + offset, BitConverter.GetBytes(1));
                WriteBytes(MK46 + offset, BitConverter.GetBytes(1));
                WriteBytes(M60 + offset, BitConverter.GetBytes(1));
                WriteBytes(BARRET + offset, BitConverter.GetBytes(1));
                WriteBytes(AWP + offset, BitConverter.GetBytes(1));
                WriteBytes(DRAGUNOV + offset, BitConverter.GetBytes(1));
                WriteBytes(AS50 + offset, BitConverter.GetBytes(1));
                WriteBytes(RSASS + offset, BitConverter.GetBytes(1));
                WriteBytes(MSR + offset, BitConverter.GetBytes(1));
                WriteBytes(USAS12 + offset, BitConverter.GetBytes(1));
                WriteBytes(KSG12 + offset, BitConverter.GetBytes(1));
                WriteBytes(SPAS12 + offset, BitConverter.GetBytes(1));
                WriteBytes(AA12 + offset, BitConverter.GetBytes(1));
                WriteBytes(STRIKER + offset, BitConverter.GetBytes(1));
                WriteBytes(M1887 + offset, BitConverter.GetBytes(1));
                WriteBytes(FMG9 + offset, BitConverter.GetBytes(1));
                WriteBytes(SKORPION + offset, BitConverter.GetBytes(1));
                WriteBytes(G18 + offset, BitConverter.GetBytes(1));
                WriteBytes(USP + offset, BitConverter.GetBytes(1));
                WriteBytes(P99 + offset, BitConverter.GetBytes(1));
                WriteBytes(MP412 + offset, BitConverter.GetBytes(1));
                WriteBytes(MAGNUM + offset, BitConverter.GetBytes(1));
                WriteBytes(FIVESEVEN + offset, BitConverter.GetBytes(1));
                WriteBytes(DEAGLE + offset, BitConverter.GetBytes(1));
                WriteBytes(SMAW + offset, BitConverter.GetBytes(1));
                WriteBytes(JAVELIN + offset, BitConverter.GetBytes(1));
                WriteBytes(XM25 + offset, BitConverter.GetBytes(1));
                WriteBytes(M320 + offset, BitConverter.GetBytes(1));
                WriteBytes(RPG + offset, BitConverter.GetBytes(1));
                WriteBytes(SHIELD + offset, BitConverter.GetBytes(1));
                MessageBox.Show(null, "Weapons has been reset.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(null, "Could not reset weapons. Make sure MW3 is running.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            UNLOCKEMBLEMS = new byte[] { 
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff
             };
            if (Process_Handle("iw5mp"))
            {
                WriteBytes(emblems, UNLOCKEMBLEMS);
                MessageBox.Show(null, "Emblems has been unlocked.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(null, "Could not unlock emblems. Make sure MW3 is running.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            UNLOCKTITLES = new byte[64] { 
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff
             };
            if (Process_Handle("iw5mp"))
            {
                WriteBytes(titles + offset, UNLOCKTITLES);
                MessageBox.Show(null, "Titles has been unlocked.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(null, "Could not unlock titles. Make sure MW3 is running.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            #region Unlock Challenges
            UNLOCKCHALLENGES = new byte[] { 
                0, 9, 9, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 4, 
                9, 9, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 4, 
                9, 9, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 4, 9, 
                9, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 4, 9, 9, 
                2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 4, 4, 9, 9, 
                2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 4, 4, 9, 9, 
                2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 4, 9, 9, 2, 
                2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 4, 9, 9, 2, 2, 
                2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 4, 9, 9, 2, 2, 2, 
                2, 2, 2, 2, 2, 2, 2, 2, 4, 4, 9, 9, 2, 2, 2, 2, 
                2, 2, 2, 2, 2, 4, 4, 9, 9, 2, 2, 2, 2, 2, 2, 2, 
                2, 2, 4, 4, 9, 9, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 
                4, 9, 9, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 4, 9, 9, 
                2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 4, 9, 9, 2, 2, 2, 
                2, 2, 2, 2, 2, 2, 4, 4, 9, 9, 2, 2, 2, 2, 2, 2, 
                2, 2, 2, 2, 4, 4, 9, 9, 2, 2, 2, 2, 2, 2, 2, 2, 
                2, 2, 4, 4, 9, 9, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 
                4, 4, 9, 9, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 4, 9, 
                9, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 4, 9, 9, 2, 2, 
                2, 2, 2, 2, 2, 4, 4, 9, 9, 2, 2, 2, 2, 2, 2, 2, 
                4, 4, 9, 9, 2, 2, 2, 2, 2, 2, 2, 4, 4, 9, 9, 2, 
                2, 2, 2, 2, 2, 2, 4, 4, 9, 9, 2, 2, 2, 2, 2, 2, 
                2, 4, 4, 9, 9, 2, 2, 2, 2, 2, 2, 2, 4, 4, 9, 9, 
                2, 2, 2, 2, 2, 2, 4, 4, 9, 9, 2, 2, 2, 2, 2, 2, 
                4, 4, 9, 9, 2, 2, 2, 2, 2, 2, 4, 4, 9, 9, 2, 2, 
                2, 2, 2, 2, 4, 4, 6, 9, 2, 2, 2, 2, 2, 2, 4, 4, 
                6, 9, 4, 4, 9, 9, 2, 2, 2, 2, 2, 2, 4, 4, 9, 9, 
                2, 2, 2, 2, 2, 2, 4, 4, 9, 9, 2, 2, 2, 2, 2, 2, 
                4, 4, 9, 9, 2, 2, 2, 2, 2, 2, 4, 4, 9, 9, 2, 2, 
                2, 2, 2, 4, 4, 9, 9, 2, 2, 2, 4, 4, 9, 9, 2, 2, 
                2, 4, 4, 9, 9, 2, 2, 2, 4, 4, 9, 9, 2, 2, 2, 2, 
                2, 4, 4, 9, 9, 2, 2, 2, 2, 2, 4, 4, 9, 4, 4, 9, 
                4, 4, 9, 7, 4, 9, 4, 4, 9, 4, 4, 9, 4, 4, 7, 7, 
                7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 
                7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 
                7, 7, 7, 7, 7, 7, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 
                9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 
                9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 
                9, 9, 9, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 
                7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 
                7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 
                7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 
                7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 
                7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 
                7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 
                7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 
                7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 
                7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 
                7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 0, 0, 0, 0, 7, 7, 
                7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0xe8, 3, 0, 0, 0xf4, 1, 0, 0, 60, 0, 0, 0, 15, 0, 
                0, 0, 20, 0, 0, 0, 20, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 10, 0, 0, 0, 0xff, 0, 0, 0, 0, 0, 0, 0, 0xe8, 3, 
                0, 0, 0xf4, 1, 0, 0, 60, 0, 0, 0, 15, 0, 0, 0, 20, 0, 
                0, 0, 20, 0, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 11, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0xe8, 3, 
                0, 0, 0xf4, 1, 0, 0, 60, 0, 0, 0, 15, 0, 0, 0, 20, 0, 
                0, 0, 20, 0, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 10, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0xe8, 3, 0, 0, 0xf4, 1, 
                0, 0, 60, 0, 0, 0, 15, 0, 0, 0, 20, 0, 0, 0, 20, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 10, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0xe8, 3, 0, 0, 0xf4, 1, 0, 0, 60, 0, 
                0, 0, 15, 0, 0, 0, 20, 0, 0, 0, 20, 0, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 11, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0xe8, 3, 0, 0, 0xf4, 1, 0, 0, 60, 0, 
                0, 0, 15, 0, 0, 0, 20, 0, 0, 0, 20, 0, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 11, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0xe8, 3, 0, 0, 0xf4, 1, 0, 0, 60, 0, 
                0, 0, 15, 0, 0, 0, 20, 0, 0, 0, 20, 0, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0xe8, 3, 0, 0, 0xf4, 1, 0, 0, 60, 0, 0, 0, 15, 0, 
                0, 0, 20, 0, 0, 0, 20, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0xe8, 3, 
                0, 0, 0xf4, 1, 0, 0, 60, 0, 0, 0, 15, 0, 0, 0, 20, 0, 
                0, 0, 20, 0, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 10, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0xe8, 3, 0, 0, 0xf4, 1, 
                0, 0, 60, 0, 0, 0, 15, 0, 0, 0, 20, 0, 0, 0, 20, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 10, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0xe8, 3, 0, 0, 0xf4, 1, 0, 0, 60, 0, 
                0, 0, 60, 0, 0, 0, 30, 0, 0, 0, 20, 0, 0, 0, 20, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 8, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0xe8, 3, 0, 0, 0xf4, 1, 
                0, 0, 60, 0, 0, 0, 60, 0, 0, 0, 30, 0, 0, 0, 20, 0, 
                0, 0, 20, 0, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0xe8, 3, 
                0, 0, 0xf4, 1, 0, 0, 60, 0, 0, 0, 60, 0, 0, 0, 30, 0, 
                0, 0, 20, 0, 0, 0, 20, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0xe8, 3, 0, 0, 0xf4, 1, 0, 0, 60, 0, 0, 0, 60, 0, 
                0, 0, 30, 0, 0, 0, 20, 0, 0, 0, 20, 0, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 8, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0xe8, 3, 0, 0, 0xf4, 1, 0, 0, 60, 0, 
                0, 0, 60, 0, 0, 0, 30, 0, 0, 0, 20, 0, 0, 0, 20, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 8, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0xe8, 3, 0, 0, 0xf4, 1, 
                0, 0, 60, 0, 0, 0, 60, 0, 0, 0, 30, 0, 0, 0, 20, 0, 
                0, 0, 20, 0, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0xe8, 3, 
                0, 0, 0xf4, 1, 0, 0, 60, 0, 0, 0, 15, 0, 0, 0, 60, 0, 
                0, 0, 20, 0, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 9, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0xe8, 3, 0, 0, 0xf4, 1, 0, 0, 60, 0, 
                0, 0, 15, 0, 0, 0, 60, 0, 0, 0, 20, 0, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 9, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0xe8, 3, 
                0, 0, 0xf4, 1, 0, 0, 60, 0, 0, 0, 15, 0, 0, 0, 60, 0, 
                0, 0, 20, 0, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 9, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0xe8, 3, 0, 0, 0xf4, 1, 0, 0, 60, 0, 
                0, 0, 15, 0, 0, 0, 60, 0, 0, 0, 20, 0, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 9, 0, 0, 0, 0, 0, 0, 0, 0xe8, 3, 0, 0, 0xf4, 1, 
                0, 0, 60, 0, 0, 0, 15, 0, 0, 0, 60, 0, 0, 0, 20, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 9, 0, 0, 0, 0, 0, 0, 0, 0xe8, 3, 
                0, 0, 0xf4, 1, 0, 0, 20, 0, 0, 0, 15, 0, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 6, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0xe8, 3, 0, 0, 0xf4, 1, 
                0, 0, 20, 0, 0, 0, 15, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 6, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0xe8, 3, 0, 0, 0xf4, 1, 0, 0, 20, 0, 
                0, 0, 15, 0, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 6, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0xe8, 3, 0, 0, 0xf4, 1, 0, 0, 20, 0, 0, 0, 15, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 6, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0xe8, 3, 
                0, 0, 0xf4, 1, 0, 0, 20, 0, 0, 0, 15, 0, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 6, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0xe8, 3, 0, 0, 0xf4, 1, 
                0, 0, 20, 0, 0, 0, 15, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 6, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0xe8, 3, 0, 0, 0xf4, 1, 0, 0, 20, 0, 
                0, 0, 15, 0, 0, 0, 60, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0xe8, 3, 
                0, 0, 0xf4, 1, 0, 0, 20, 0, 0, 0, 15, 0, 0, 0, 60, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 5, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0xe8, 3, 0, 0, 0xf4, 1, 0, 0, 20, 0, 
                0, 0, 15, 0, 0, 0, 60, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0xe8, 3, 
                0, 0, 0xf4, 1, 0, 0, 20, 0, 0, 0, 15, 0, 0, 0, 60, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 5, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0xe8, 3, 0, 0, 0xf4, 1, 0, 0, 20, 0, 
                0, 0, 15, 0, 0, 0, 60, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0xc4, 9, 
                0, 0, 0xf4, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0xe8, 3, 
                0, 0, 0xf4, 1, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 5, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0xe8, 3, 0, 0, 0xf4, 1, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0xe8, 3, 
                0, 0, 0xf4, 1, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 5, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0xe8, 3, 0, 0, 0xf4, 1, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0xe8, 3, 
                0, 0, 0xf4, 1, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0xe8, 3, 0, 0, 0xf4, 1, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0xe8, 3, 
                0, 0, 0xf4, 1, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 2, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0xc4, 9, 0, 0, 0xf4, 1, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 2, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0xe8, 3, 0, 0, 0xf4, 1, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 4, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0xe8, 3, 0, 0, 0xf4, 1, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0xb0, 4, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0xb0, 4, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 250, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0xb0, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0xb0, 4, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0xb0, 4, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0x72, 6, 0, 0xee, 2, 0, 0, 0xf4, 1, 
                0, 0, 250, 0, 0, 0, 0xee, 2, 0, 0, 0xf4, 1, 0, 0, 100, 0, 
                0, 0, 0xf4, 1, 0, 0, 0xee, 2, 0, 0, 100, 0, 0, 0, 250, 0, 
                0, 0, 250, 0, 0, 0, 0xee, 2, 0, 0, 250, 0, 0, 0, 0xee, 2, 
                0, 0, 250, 0, 0, 0, 0xee, 2, 0, 0, 0xee, 2, 0, 0, 250, 0, 
                0, 0, 0xee, 2, 0, 0, 0xf4, 1, 0, 0, 100, 0, 0, 0, 0xf4, 1, 
                0, 0, 0xee, 2, 0, 0, 100, 0, 0, 0, 100, 0, 0, 0, 0xee, 2, 
                0, 0, 0xf4, 1, 0, 0, 100, 0, 0, 0, 100, 0, 0, 0, 0xee, 2, 
                0, 0, 0xee, 2, 0, 0, 0xee, 2, 0, 0, 0xee, 2, 0, 0, 0xee, 2, 
                0, 0, 0xee, 2, 0, 0, 0xee, 2, 0, 0, 0xee, 2, 0, 0, 0xee, 2, 
                0, 0, 0xee, 2, 0, 0, 0xf4, 1, 0, 0, 0xf4, 1, 0, 0, 0xf4, 1, 
                0, 0, 0xf4, 1, 0, 0, 0xf4, 1, 0, 0, 0xf4, 1, 0, 0, 0xf4, 1, 
                0, 0, 250, 0, 0, 0, 250, 0, 0, 0, 250, 0, 0, 0, 250, 0, 
                0, 0, 250, 0, 0, 0, 250, 0, 0, 0, 250, 0, 0, 0, 250, 0, 
                0, 0, 0xe8, 3, 0, 0, 0xf4, 1, 0, 0, 0xf4, 1, 0, 0, 0xf4, 1, 
                0, 0, 0xf4, 1, 0, 0, 0xf4, 1, 0, 0, 0xf4, 1, 0, 0, 0xf4, 1, 
                0, 0, 0xf4, 1, 0, 0, 250, 0, 0, 0, 250, 0, 0, 0, 250, 0, 
                0, 0, 250, 0, 0, 0, 0xe8, 3, 0, 0, 0xf4, 1, 0, 0, 0xf4, 1, 
                0, 0, 0xf4, 1, 0, 0, 0xf4, 1, 0, 0, 0xf4, 1, 0, 0, 0xf4, 1, 
                0, 0, 0xf4, 1, 0, 0, 0xf4, 1, 0, 0, 0xf4, 1, 0, 0, 0xf4, 1, 
                0, 0, 0xf4, 1, 0, 0, 0xf4, 1, 0, 0, 0xf4, 1, 0, 0, 0xf4, 1, 
                0, 0, 250, 0, 0, 0, 220, 5, 0, 0, 0, 0, 0, 0, 50, 0, 
                0, 0, 30, 0, 0, 0, 30, 0, 0, 0, 30, 0, 0, 0, 30, 0, 
                0, 0, 30, 0, 0, 0, 50, 0, 0, 0, 50, 0, 0, 0, 50, 0, 
                0, 0, 50, 0, 0, 0, 50, 0, 0, 0, 50, 0, 0, 0, 50, 0, 
                0, 0, 50, 0, 0, 0, 1, 0, 0, 0, 0x19, 0, 0, 0, 0x19, 0, 
                0, 0, 1, 0, 0, 0, 9, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 
                0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 
                0, 0, 1, 0, 0, 0, 20, 0, 0, 0, 1, 0, 0, 0, 1, 0, 
                0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 200, 0, 0, 0, 10, 0, 
                0, 0, 10, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 
                0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 
                0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 
                0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 
                0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 
                0, 0, 30, 0, 0, 0, 30, 0, 0, 0, 30, 0, 0, 0, 15, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 
                0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 50, 0, 0, 0, 50, 0, 
                0, 0, 50, 0, 0, 0, 50, 0, 0, 0, 50, 0, 0, 0, 50, 0, 
                0, 0, 50, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 
                0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 
                0, 0, 1, 0, 0, 0, 0x19, 0, 0, 0, 0x19, 0, 0, 0, 1, 0, 
                0, 0, 1, 0, 0, 0, 0x19, 0, 0, 0, 0x19, 0, 0, 0, 1, 0, 
                0, 0, 0x19, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 
                0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 20, 0, 
                0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 20, 0, 0, 0, 20, 0, 
                0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 
                0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 
                0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 
                0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 
                0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0
             };
#endregion
           if (Process_Handle("iw5mp"))
           {
               WriteBytes(challenges, UNLOCKCHALLENGES);
               MessageBox.Show(null, "Challenges has been unlocked.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
           }
           else
           {
               MessageBox.Show(null, "Could not unlock challenges. Make sure MW3 is running.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
           }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("Changelog.txt", "");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            // Damn you WonseK for forcing me to do this...
            if (Process_Handle("iw5mp"))
            {
                if (comboBox3.Text == "M4A1")
                {
                    WriteBytes(M4A1 + offset, BitConverter.GetBytes(9999999));
                }
                if (comboBox3.Text == "M16A4")
                {
                    WriteBytes(M16A4 + offset, BitConverter.GetBytes(9999999));
                }
                if (comboBox3.Text == "SCAR")
                {
                    WriteBytes(SCAR + offset, BitConverter.GetBytes(9999999));
                }
                if (comboBox3.Text == "CM901")
                {
                    WriteBytes(CM901 + offset, BitConverter.GetBytes(9999999));
                }
                if (comboBox3.Text == "TYPE95")
                {
                    WriteBytes(TYPE95 + offset, BitConverter.GetBytes(9999999));
                }
                if (comboBox3.Text == "G36C")
                {
                    WriteBytes(G36C + offset, BitConverter.GetBytes(9999999));
                }
                if (comboBox3.Text == "ACR")
                {
                    WriteBytes(ACR + offset, BitConverter.GetBytes(9999999));
                }
                if (comboBox3.Text == "MK14")
                {
                    WriteBytes(MK14 + offset, BitConverter.GetBytes(9999999));
                }
                if (comboBox3.Text == "AK47")
                {
                    WriteBytes(AK47 + offset, BitConverter.GetBytes(9999999));
                }
                if (comboBox3.Text == "FAD")
                {
                    WriteBytes(FAD + offset, BitConverter.GetBytes(9999999));
                }
                if (comboBox3.Text == "MP5")
                {
                    WriteBytes(MP5 + offset, BitConverter.GetBytes(9999999));
                }
                if (comboBox3.Text == "UMP")
                {
                    WriteBytes(UMP + offset, BitConverter.GetBytes(9999999));
                }
                if (comboBox3.Text == "PP90")
                {
                    WriteBytes(PP90 + offset, BitConverter.GetBytes(9999999));
                }
                if (comboBox3.Text == "P90")
                {
                    WriteBytes(P90 + offset, BitConverter.GetBytes(9999999));
                }
                if (comboBox3.Text == "PM9")
                {
                    WriteBytes(PM9 + offset, BitConverter.GetBytes(9999999));
                }
                if (comboBox3.Text == "MP7")
                {
                    WriteBytes(MP7 + offset, BitConverter.GetBytes(9999999));
                }
                if (comboBox3.Text == "L86")
                {
                    WriteBytes(L86 + offset, BitConverter.GetBytes(9999999));
                }
                if (comboBox3.Text == "MG36")
                {
                    WriteBytes(MG36 + offset, BitConverter.GetBytes(9999999));
                }
                if (comboBox3.Text == "PKP")
                {
                    WriteBytes(PKP + offset, BitConverter.GetBytes(9999999));
                }
                if (comboBox3.Text == "MK46")
                {
                    WriteBytes(MK46 + offset, BitConverter.GetBytes(9999999));
                }
                if (comboBox3.Text == "M60")
                {
                    WriteBytes(M60 + offset, BitConverter.GetBytes(9999999));
                }
                if (comboBox3.Text == "BARRET")
                {
                    WriteBytes(BARRET + offset, BitConverter.GetBytes(9999999));
                }
                if (comboBox3.Text == "L118A")
                {
                    WriteBytes(AWP + offset, BitConverter.GetBytes(9999999));
                }
                if (comboBox3.Text == "DRAGUNOV")
                {
                    WriteBytes(DRAGUNOV + offset, BitConverter.GetBytes(9999999));
                }
                if (comboBox3.Text == "AS50")
                {
                    WriteBytes(AS50 + offset, BitConverter.GetBytes(9999999));
                }
                if (comboBox3.Text == "RSASS")
                {
                    WriteBytes(RSASS + offset, BitConverter.GetBytes(9999999));
                }
                if (comboBox3.Text == "MSR")
                {
                    WriteBytes(MSR + offset, BitConverter.GetBytes(9999999));
                }
                if (comboBox3.Text == "USAS12")
                {
                    WriteBytes(USAS12 + offset, BitConverter.GetBytes(9999999));
                }
                if (comboBox3.Text == "KSG12")
                {
                    WriteBytes(KSG12 + offset, BitConverter.GetBytes(9999999));
                }
                if (comboBox3.Text == "SPAS12")
                {
                    WriteBytes(SPAS12 + offset, BitConverter.GetBytes(9999999));
                }
                if (comboBox3.Text == "AA12")
                {
                    WriteBytes(AA12 + offset, BitConverter.GetBytes(9999999));
                }
                if (comboBox3.Text == "STRIKER")
                {
                    WriteBytes(STRIKER + offset, BitConverter.GetBytes(9999999));
                }
                if (comboBox3.Text == "M1887")
                {
                    WriteBytes(M1887 + offset, BitConverter.GetBytes(9999999));
                }
                if (comboBox3.Text == "FMG9")
                {
                    WriteBytes(FMG9 + offset, BitConverter.GetBytes(9999999));
                }
                if (comboBox3.Text == "SKORPION")
                {
                    WriteBytes(SKORPION + offset, BitConverter.GetBytes(9999999));
                }
                if (comboBox3.Text == "G18")
                {
                    WriteBytes(G18 + offset, BitConverter.GetBytes(9999999));
                }
                if (comboBox3.Text == "USP")
                {
                    WriteBytes(USP + offset, BitConverter.GetBytes(9999999));
                }
                if (comboBox3.Text == "P99")
                {
                    WriteBytes(P99 + offset, BitConverter.GetBytes(9999999));
                }
                if (comboBox3.Text == "MP412")
                {
                    WriteBytes(MP412 + offset, BitConverter.GetBytes(9999999));
                }
                if (comboBox3.Text == "MAGNUM")
                {
                    WriteBytes(MAGNUM + offset, BitConverter.GetBytes(9999999));
                }
                if (comboBox3.Text == "DEAGLE")
                {
                    WriteBytes(DEAGLE + offset, BitConverter.GetBytes(9999999));
                }
                if (comboBox3.Text == "FIVESEVEN")
                {
                    WriteBytes(FIVESEVEN + offset, BitConverter.GetBytes(9999999));
                }
                if (comboBox3.Text == "SMAW")
                {
                    WriteBytes(SMAW + offset, BitConverter.GetBytes(9999999));
                }
                if (comboBox3.Text == "JAVELIN")
                {
                    WriteBytes(JAVELIN + offset, BitConverter.GetBytes(9999999));
                }
                if (comboBox3.Text == "XM25")
                {
                    WriteBytes(XM25 + offset, BitConverter.GetBytes(9999999));
                }
                if (comboBox3.Text == "M320")
                {
                    WriteBytes(M320 + offset, BitConverter.GetBytes(9999999));
                }
                if (comboBox3.Text == "RPG")
                {
                    WriteBytes(RPG + offset, BitConverter.GetBytes(9999999));
                }
                if (comboBox3.Text == "RIOT SHIELD")
                {
                    WriteBytes(SHIELD + offset, BitConverter.GetBytes(9999999));

                }
                MessageBox.Show(null, comboBox3.Text.ToString() + " has been set to level 31." + Environment.NewLine + "For changes to take effect, enter a game and get a kill.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(null, "Could not set " + comboBox3.Text.ToString() + " to level 31. Make sure MW3 is running.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if(Process_Handle("iw5mp"))
            {
                if (comboBox3.Text == "M4A1")
                {
                    WriteBytes(M4A1 + offset, BitConverter.GetBytes(1));
                }
                if (comboBox3.Text == "M16A4")
                {
                    WriteBytes(M16A4 + offset, BitConverter.GetBytes(1));
                }
                if (comboBox3.Text == "SCAR")
                {
                    WriteBytes(SCAR + offset, BitConverter.GetBytes(1));
                }
                if (comboBox3.Text == "CM901")
                {
                    WriteBytes(CM901 + offset, BitConverter.GetBytes(1));
                }
                if (comboBox3.Text == "TYPE95")
                {
                    WriteBytes(TYPE95 + offset, BitConverter.GetBytes(1));
                }
                if (comboBox3.Text == "G36C")
                {
                    WriteBytes(G36C + offset, BitConverter.GetBytes(1));
                }
                if (comboBox3.Text == "ACR")
                {
                    WriteBytes(ACR + offset, BitConverter.GetBytes(1));
                }
                if (comboBox3.Text == "MK14")
                {
                    WriteBytes(MK14 + offset, BitConverter.GetBytes(1));
                }
                if (comboBox3.Text == "AK47")
                {
                    WriteBytes(AK47 + offset, BitConverter.GetBytes(1));
                }
                if (comboBox3.Text == "FAD")
                {
                    WriteBytes(FAD + offset, BitConverter.GetBytes(1));
                }
                if (comboBox3.Text == "MP5")
                {
                    WriteBytes(MP5 + offset, BitConverter.GetBytes(1));
                }
                if (comboBox3.Text == "UMP")
                {
                    WriteBytes(UMP + offset, BitConverter.GetBytes(1));
                }
                if (comboBox3.Text == "PP90")
                {
                    WriteBytes(PP90 + offset, BitConverter.GetBytes(1));
                }
                if (comboBox3.Text == "P90")
                {
                    WriteBytes(P90 + offset, BitConverter.GetBytes(1));
                }
                if (comboBox3.Text == "PM9")
                {
                    WriteBytes(PM9 + offset, BitConverter.GetBytes(1));
                }
                if (comboBox3.Text == "MP7")
                {
                    WriteBytes(MP7 + offset, BitConverter.GetBytes(1));
                }
                if (comboBox3.Text == "L86")
                {
                    WriteBytes(L86 + offset, BitConverter.GetBytes(1));
                }
                if (comboBox3.Text == "MG36")
                {
                    WriteBytes(MG36 + offset, BitConverter.GetBytes(1));
                }
                if (comboBox3.Text == "PKP")
                {
                    WriteBytes(PKP + offset, BitConverter.GetBytes(1));
                }
                if (comboBox3.Text == "MK46")
                {
                    WriteBytes(MK46 + offset, BitConverter.GetBytes(1));
                }
                if (comboBox3.Text == "M60")
                {
                    WriteBytes(M60 + offset, BitConverter.GetBytes(1));
                }
                if (comboBox3.Text == "BARRET")
                {
                    WriteBytes(BARRET + offset, BitConverter.GetBytes(1));
                }
                if (comboBox3.Text == "L118A")
                {
                    WriteBytes(AWP + offset, BitConverter.GetBytes(1));
                }
                if (comboBox3.Text == "DRAGUNOV")
                {
                    WriteBytes(DRAGUNOV + offset, BitConverter.GetBytes(1));
                }
                if (comboBox3.Text == "AS50")
                {
                    WriteBytes(AS50 + offset, BitConverter.GetBytes(1));
                }
                if (comboBox3.Text == "RSASS")
                {
                    WriteBytes(RSASS + offset, BitConverter.GetBytes(1));
                }
                if (comboBox3.Text == "MSR")
                {
                    WriteBytes(MSR + offset, BitConverter.GetBytes(1));
                }
                if (comboBox3.Text == "USAS12")
                {
                    WriteBytes(USAS12 + offset, BitConverter.GetBytes(1));
                }
                if (comboBox3.Text == "KSG12")
                {
                    WriteBytes(KSG12 + offset, BitConverter.GetBytes(1));
                }
                if (comboBox3.Text == "SPAS12")
                {
                    WriteBytes(SPAS12 + offset, BitConverter.GetBytes(1));
                }
                if (comboBox3.Text == "AA12")
                {
                    WriteBytes(AA12 + offset, BitConverter.GetBytes(1));
                }
                if (comboBox3.Text == "STRIKER")
                {
                    WriteBytes(STRIKER + offset, BitConverter.GetBytes(1));
                }
                if (comboBox3.Text == "M1887")
                {
                    WriteBytes(M1887 + offset, BitConverter.GetBytes(1));
                }
                if (comboBox3.Text == "FMG9")
                {
                    WriteBytes(FMG9 + offset, BitConverter.GetBytes(1));
                }
                if (comboBox3.Text == "SKORPION")
                {
                    WriteBytes(SKORPION + offset, BitConverter.GetBytes(1));
                }
                if (comboBox3.Text == "G18")
                {
                    WriteBytes(G18 + offset, BitConverter.GetBytes(1));
                }
                if (comboBox3.Text == "USP")
                {
                    WriteBytes(USP + offset, BitConverter.GetBytes(1));
                }
                if (comboBox3.Text == "P99")
                {
                    WriteBytes(P99 + offset, BitConverter.GetBytes(1));
                }
                if (comboBox3.Text == "MP412")
                {
                    WriteBytes(MP412 + offset, BitConverter.GetBytes(1));
                }
                if (comboBox3.Text == "MAGNUM")
                {
                    WriteBytes(MAGNUM + offset, BitConverter.GetBytes(1));
                }
                if (comboBox3.Text == "DEAGLE")
                {
                    WriteBytes(DEAGLE + offset, BitConverter.GetBytes(1));
                }
                if (comboBox3.Text == "FIVESEVEN")
                {
                    WriteBytes(FIVESEVEN + offset, BitConverter.GetBytes(1));
                }
                if (comboBox3.Text == "SMAW")
                {
                    WriteBytes(SMAW + offset, BitConverter.GetBytes(1));
                }
                if (comboBox3.Text == "JAVELIN")
                {
                    WriteBytes(JAVELIN + offset, BitConverter.GetBytes(1));
                }
                if (comboBox3.Text == "XM25")
                {
                    WriteBytes(XM25 + offset, BitConverter.GetBytes(1));
                }
                if (comboBox3.Text == "M320")
                {
                    WriteBytes(M320 + offset, BitConverter.GetBytes(1));
                }
                if (comboBox3.Text == "RPG")
                {
                    WriteBytes(RPG + offset, BitConverter.GetBytes(1));
                }
                if (comboBox3.Text == "RIOT SHIELD")
                {
                    WriteBytes(SHIELD + offset, BitConverter.GetBytes(1));
                }
                MessageBox.Show(null, comboBox3.Text.ToString() + " has been reset to level 1." + Environment.NewLine + "For changes to take effect, enter a game and get a kill.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(null, "Could reset " + comboBox3.Text.ToString() + " to level 1. Make sure MW3 is running.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            }
        

        private void button14_Click(object sender, EventArgs e)
        {
            // UNLOCK EVERYTHING 

            if (Process_Handle("iw5mp"))
            {
                // Weapons
                WriteBytes(M4A1 + offset, BitConverter.GetBytes(9999999));
                WriteBytes(M16A4 + offset, BitConverter.GetBytes(9999999));
                WriteBytes(SCAR + offset, BitConverter.GetBytes(9999999));
                WriteBytes(CM901 + offset, BitConverter.GetBytes(9999999));
                WriteBytes(TYPE95 + offset, BitConverter.GetBytes(9999999));
                WriteBytes(G36C + offset, BitConverter.GetBytes(9999999));
                WriteBytes(ACR + offset, BitConverter.GetBytes(9999999));
                WriteBytes(MK14 + offset, BitConverter.GetBytes(9999999));
                WriteBytes(AK47 + offset, BitConverter.GetBytes(9999999));
                WriteBytes(FAD + offset, BitConverter.GetBytes(9999999));
                WriteBytes(MP5 + offset, BitConverter.GetBytes(9999999));
                WriteBytes(UMP + offset, BitConverter.GetBytes(9999999));
                WriteBytes(PP90 + offset, BitConverter.GetBytes(9999999));
                WriteBytes(P90 + offset, BitConverter.GetBytes(9999999));
                WriteBytes(PM9 + offset, BitConverter.GetBytes(9999999));
                WriteBytes(MP7 + offset, BitConverter.GetBytes(9999999));
                WriteBytes(L86 + offset, BitConverter.GetBytes(9999999));
                WriteBytes(MG36 + offset, BitConverter.GetBytes(9999999));
                WriteBytes(PKP + offset, BitConverter.GetBytes(9999999));
                WriteBytes(MK46 + offset, BitConverter.GetBytes(9999999));
                WriteBytes(M60 + offset, BitConverter.GetBytes(9999999));
                WriteBytes(BARRET + offset, BitConverter.GetBytes(9999999));
                WriteBytes(AWP + offset, BitConverter.GetBytes(9999999));
                WriteBytes(DRAGUNOV + offset, BitConverter.GetBytes(9999999));
                WriteBytes(AS50 + offset, BitConverter.GetBytes(9999999));
                WriteBytes(RSASS + offset, BitConverter.GetBytes(9999999));
                WriteBytes(MSR + offset, BitConverter.GetBytes(9999999));
                WriteBytes(USAS12 + offset, BitConverter.GetBytes(9999999));
                WriteBytes(KSG12 + offset, BitConverter.GetBytes(9999999));
                WriteBytes(SPAS12 + offset, BitConverter.GetBytes(9999999));
                WriteBytes(AA12 + offset, BitConverter.GetBytes(9999999));
                WriteBytes(STRIKER + offset, BitConverter.GetBytes(9999999));
                WriteBytes(M1887 + offset, BitConverter.GetBytes(9999999));
                WriteBytes(FMG9 + offset, BitConverter.GetBytes(9999999));
                WriteBytes(SKORPION + offset, BitConverter.GetBytes(9999999));
                WriteBytes(G18 + offset, BitConverter.GetBytes(9999999));
                WriteBytes(USP + offset, BitConverter.GetBytes(9999999));
                WriteBytes(P99 + offset, BitConverter.GetBytes(9999999));
                WriteBytes(MP412 + offset, BitConverter.GetBytes(9999999));
                WriteBytes(MAGNUM + offset, BitConverter.GetBytes(9999999));
                WriteBytes(FIVESEVEN + offset, BitConverter.GetBytes(9999999));
                WriteBytes(DEAGLE + offset, BitConverter.GetBytes(9999999));
                WriteBytes(SMAW + offset, BitConverter.GetBytes(9999999));
                WriteBytes(JAVELIN + offset, BitConverter.GetBytes(9999999));
                WriteBytes(XM25 + offset, BitConverter.GetBytes(9999999));
                WriteBytes(M320 + offset, BitConverter.GetBytes(9999999));
                WriteBytes(RPG + offset, BitConverter.GetBytes(9999999));
                WriteBytes(SHIELD + offset, BitConverter.GetBytes(9999999));

                // Prestige
                WriteInteger(PRESTIGE, 20);

                // EXP
                WriteInteger(EXP, 1746200);

                // Classes
                WriteInteger(CLASSES, 15);

                // Challenges
                #region Unlock Challenges
                UNLOCKCHALLENGES = new byte[] { 
                0, 9, 9, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 4, 
                9, 9, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 4, 
                9, 9, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 4, 9, 
                9, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 4, 9, 9, 
                2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 4, 4, 9, 9, 
                2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 4, 4, 9, 9, 
                2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 4, 9, 9, 2, 
                2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 4, 9, 9, 2, 2, 
                2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 4, 9, 9, 2, 2, 2, 
                2, 2, 2, 2, 2, 2, 2, 2, 4, 4, 9, 9, 2, 2, 2, 2, 
                2, 2, 2, 2, 2, 4, 4, 9, 9, 2, 2, 2, 2, 2, 2, 2, 
                2, 2, 4, 4, 9, 9, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 
                4, 9, 9, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 4, 9, 9, 
                2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 4, 9, 9, 2, 2, 2, 
                2, 2, 2, 2, 2, 2, 4, 4, 9, 9, 2, 2, 2, 2, 2, 2, 
                2, 2, 2, 2, 4, 4, 9, 9, 2, 2, 2, 2, 2, 2, 2, 2, 
                2, 2, 4, 4, 9, 9, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 
                4, 4, 9, 9, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 4, 9, 
                9, 2, 2, 2, 2, 2, 2, 2, 2, 2, 4, 4, 9, 9, 2, 2, 
                2, 2, 2, 2, 2, 4, 4, 9, 9, 2, 2, 2, 2, 2, 2, 2, 
                4, 4, 9, 9, 2, 2, 2, 2, 2, 2, 2, 4, 4, 9, 9, 2, 
                2, 2, 2, 2, 2, 2, 4, 4, 9, 9, 2, 2, 2, 2, 2, 2, 
                2, 4, 4, 9, 9, 2, 2, 2, 2, 2, 2, 2, 4, 4, 9, 9, 
                2, 2, 2, 2, 2, 2, 4, 4, 9, 9, 2, 2, 2, 2, 2, 2, 
                4, 4, 9, 9, 2, 2, 2, 2, 2, 2, 4, 4, 9, 9, 2, 2, 
                2, 2, 2, 2, 4, 4, 6, 9, 2, 2, 2, 2, 2, 2, 4, 4, 
                6, 9, 4, 4, 9, 9, 2, 2, 2, 2, 2, 2, 4, 4, 9, 9, 
                2, 2, 2, 2, 2, 2, 4, 4, 9, 9, 2, 2, 2, 2, 2, 2, 
                4, 4, 9, 9, 2, 2, 2, 2, 2, 2, 4, 4, 9, 9, 2, 2, 
                2, 2, 2, 4, 4, 9, 9, 2, 2, 2, 4, 4, 9, 9, 2, 2, 
                2, 4, 4, 9, 9, 2, 2, 2, 4, 4, 9, 9, 2, 2, 2, 2, 
                2, 4, 4, 9, 9, 2, 2, 2, 2, 2, 4, 4, 9, 4, 4, 9, 
                4, 4, 9, 7, 4, 9, 4, 4, 9, 4, 4, 9, 4, 4, 7, 7, 
                7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 
                7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 
                7, 7, 7, 7, 7, 7, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 
                9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 
                9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 
                9, 9, 9, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 
                7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 
                7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 
                7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 
                7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 
                7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 
                7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 
                7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 
                7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 
                7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 
                7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 0, 0, 0, 0, 7, 7, 
                7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0xe8, 3, 0, 0, 0xf4, 1, 0, 0, 60, 0, 0, 0, 15, 0, 
                0, 0, 20, 0, 0, 0, 20, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 10, 0, 0, 0, 0xff, 0, 0, 0, 0, 0, 0, 0, 0xe8, 3, 
                0, 0, 0xf4, 1, 0, 0, 60, 0, 0, 0, 15, 0, 0, 0, 20, 0, 
                0, 0, 20, 0, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 11, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0xe8, 3, 
                0, 0, 0xf4, 1, 0, 0, 60, 0, 0, 0, 15, 0, 0, 0, 20, 0, 
                0, 0, 20, 0, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 10, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0xe8, 3, 0, 0, 0xf4, 1, 
                0, 0, 60, 0, 0, 0, 15, 0, 0, 0, 20, 0, 0, 0, 20, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 10, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0xe8, 3, 0, 0, 0xf4, 1, 0, 0, 60, 0, 
                0, 0, 15, 0, 0, 0, 20, 0, 0, 0, 20, 0, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 11, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0xe8, 3, 0, 0, 0xf4, 1, 0, 0, 60, 0, 
                0, 0, 15, 0, 0, 0, 20, 0, 0, 0, 20, 0, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 11, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0xe8, 3, 0, 0, 0xf4, 1, 0, 0, 60, 0, 
                0, 0, 15, 0, 0, 0, 20, 0, 0, 0, 20, 0, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0xe8, 3, 0, 0, 0xf4, 1, 0, 0, 60, 0, 0, 0, 15, 0, 
                0, 0, 20, 0, 0, 0, 20, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0xe8, 3, 
                0, 0, 0xf4, 1, 0, 0, 60, 0, 0, 0, 15, 0, 0, 0, 20, 0, 
                0, 0, 20, 0, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 10, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0xe8, 3, 0, 0, 0xf4, 1, 
                0, 0, 60, 0, 0, 0, 15, 0, 0, 0, 20, 0, 0, 0, 20, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 10, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0xe8, 3, 0, 0, 0xf4, 1, 0, 0, 60, 0, 
                0, 0, 60, 0, 0, 0, 30, 0, 0, 0, 20, 0, 0, 0, 20, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 8, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0xe8, 3, 0, 0, 0xf4, 1, 
                0, 0, 60, 0, 0, 0, 60, 0, 0, 0, 30, 0, 0, 0, 20, 0, 
                0, 0, 20, 0, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0xe8, 3, 
                0, 0, 0xf4, 1, 0, 0, 60, 0, 0, 0, 60, 0, 0, 0, 30, 0, 
                0, 0, 20, 0, 0, 0, 20, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0xe8, 3, 0, 0, 0xf4, 1, 0, 0, 60, 0, 0, 0, 60, 0, 
                0, 0, 30, 0, 0, 0, 20, 0, 0, 0, 20, 0, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 8, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0xe8, 3, 0, 0, 0xf4, 1, 0, 0, 60, 0, 
                0, 0, 60, 0, 0, 0, 30, 0, 0, 0, 20, 0, 0, 0, 20, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 8, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0xe8, 3, 0, 0, 0xf4, 1, 
                0, 0, 60, 0, 0, 0, 60, 0, 0, 0, 30, 0, 0, 0, 20, 0, 
                0, 0, 20, 0, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0xe8, 3, 
                0, 0, 0xf4, 1, 0, 0, 60, 0, 0, 0, 15, 0, 0, 0, 60, 0, 
                0, 0, 20, 0, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 9, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0xe8, 3, 0, 0, 0xf4, 1, 0, 0, 60, 0, 
                0, 0, 15, 0, 0, 0, 60, 0, 0, 0, 20, 0, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 9, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0xe8, 3, 
                0, 0, 0xf4, 1, 0, 0, 60, 0, 0, 0, 15, 0, 0, 0, 60, 0, 
                0, 0, 20, 0, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 9, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0xe8, 3, 0, 0, 0xf4, 1, 0, 0, 60, 0, 
                0, 0, 15, 0, 0, 0, 60, 0, 0, 0, 20, 0, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 9, 0, 0, 0, 0, 0, 0, 0, 0xe8, 3, 0, 0, 0xf4, 1, 
                0, 0, 60, 0, 0, 0, 15, 0, 0, 0, 60, 0, 0, 0, 20, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 9, 0, 0, 0, 0, 0, 0, 0, 0xe8, 3, 
                0, 0, 0xf4, 1, 0, 0, 20, 0, 0, 0, 15, 0, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 6, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0xe8, 3, 0, 0, 0xf4, 1, 
                0, 0, 20, 0, 0, 0, 15, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 6, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0xe8, 3, 0, 0, 0xf4, 1, 0, 0, 20, 0, 
                0, 0, 15, 0, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 6, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0xe8, 3, 0, 0, 0xf4, 1, 0, 0, 20, 0, 0, 0, 15, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 6, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0xe8, 3, 
                0, 0, 0xf4, 1, 0, 0, 20, 0, 0, 0, 15, 0, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 6, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0xe8, 3, 0, 0, 0xf4, 1, 
                0, 0, 20, 0, 0, 0, 15, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 6, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0xe8, 3, 0, 0, 0xf4, 1, 0, 0, 20, 0, 
                0, 0, 15, 0, 0, 0, 60, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0xe8, 3, 
                0, 0, 0xf4, 1, 0, 0, 20, 0, 0, 0, 15, 0, 0, 0, 60, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 5, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0xe8, 3, 0, 0, 0xf4, 1, 0, 0, 20, 0, 
                0, 0, 15, 0, 0, 0, 60, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0xe8, 3, 
                0, 0, 0xf4, 1, 0, 0, 20, 0, 0, 0, 15, 0, 0, 0, 60, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 5, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0xe8, 3, 0, 0, 0xf4, 1, 0, 0, 20, 0, 
                0, 0, 15, 0, 0, 0, 60, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0xc4, 9, 
                0, 0, 0xf4, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0xe8, 3, 
                0, 0, 0xf4, 1, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 5, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0xe8, 3, 0, 0, 0xf4, 1, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0xe8, 3, 
                0, 0, 0xf4, 1, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 5, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0xe8, 3, 0, 0, 0xf4, 1, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0xe8, 3, 
                0, 0, 0xf4, 1, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0xe8, 3, 0, 0, 0xf4, 1, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0xe8, 3, 
                0, 0, 0xf4, 1, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 2, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0xc4, 9, 0, 0, 0xf4, 1, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 2, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0xe8, 3, 0, 0, 0xf4, 1, 0, 0, 40, 0, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 4, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0xe8, 3, 0, 0, 0xf4, 1, 
                0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 0, 0, 40, 0, 
                0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0xb0, 4, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0xb0, 4, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 250, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0xb0, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0xb0, 4, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0xb0, 4, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0x72, 6, 0, 0xee, 2, 0, 0, 0xf4, 1, 
                0, 0, 250, 0, 0, 0, 0xee, 2, 0, 0, 0xf4, 1, 0, 0, 100, 0, 
                0, 0, 0xf4, 1, 0, 0, 0xee, 2, 0, 0, 100, 0, 0, 0, 250, 0, 
                0, 0, 250, 0, 0, 0, 0xee, 2, 0, 0, 250, 0, 0, 0, 0xee, 2, 
                0, 0, 250, 0, 0, 0, 0xee, 2, 0, 0, 0xee, 2, 0, 0, 250, 0, 
                0, 0, 0xee, 2, 0, 0, 0xf4, 1, 0, 0, 100, 0, 0, 0, 0xf4, 1, 
                0, 0, 0xee, 2, 0, 0, 100, 0, 0, 0, 100, 0, 0, 0, 0xee, 2, 
                0, 0, 0xf4, 1, 0, 0, 100, 0, 0, 0, 100, 0, 0, 0, 0xee, 2, 
                0, 0, 0xee, 2, 0, 0, 0xee, 2, 0, 0, 0xee, 2, 0, 0, 0xee, 2, 
                0, 0, 0xee, 2, 0, 0, 0xee, 2, 0, 0, 0xee, 2, 0, 0, 0xee, 2, 
                0, 0, 0xee, 2, 0, 0, 0xf4, 1, 0, 0, 0xf4, 1, 0, 0, 0xf4, 1, 
                0, 0, 0xf4, 1, 0, 0, 0xf4, 1, 0, 0, 0xf4, 1, 0, 0, 0xf4, 1, 
                0, 0, 250, 0, 0, 0, 250, 0, 0, 0, 250, 0, 0, 0, 250, 0, 
                0, 0, 250, 0, 0, 0, 250, 0, 0, 0, 250, 0, 0, 0, 250, 0, 
                0, 0, 0xe8, 3, 0, 0, 0xf4, 1, 0, 0, 0xf4, 1, 0, 0, 0xf4, 1, 
                0, 0, 0xf4, 1, 0, 0, 0xf4, 1, 0, 0, 0xf4, 1, 0, 0, 0xf4, 1, 
                0, 0, 0xf4, 1, 0, 0, 250, 0, 0, 0, 250, 0, 0, 0, 250, 0, 
                0, 0, 250, 0, 0, 0, 0xe8, 3, 0, 0, 0xf4, 1, 0, 0, 0xf4, 1, 
                0, 0, 0xf4, 1, 0, 0, 0xf4, 1, 0, 0, 0xf4, 1, 0, 0, 0xf4, 1, 
                0, 0, 0xf4, 1, 0, 0, 0xf4, 1, 0, 0, 0xf4, 1, 0, 0, 0xf4, 1, 
                0, 0, 0xf4, 1, 0, 0, 0xf4, 1, 0, 0, 0xf4, 1, 0, 0, 0xf4, 1, 
                0, 0, 250, 0, 0, 0, 220, 5, 0, 0, 0, 0, 0, 0, 50, 0, 
                0, 0, 30, 0, 0, 0, 30, 0, 0, 0, 30, 0, 0, 0, 30, 0, 
                0, 0, 30, 0, 0, 0, 50, 0, 0, 0, 50, 0, 0, 0, 50, 0, 
                0, 0, 50, 0, 0, 0, 50, 0, 0, 0, 50, 0, 0, 0, 50, 0, 
                0, 0, 50, 0, 0, 0, 1, 0, 0, 0, 0x19, 0, 0, 0, 0x19, 0, 
                0, 0, 1, 0, 0, 0, 9, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 
                0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 
                0, 0, 1, 0, 0, 0, 20, 0, 0, 0, 1, 0, 0, 0, 1, 0, 
                0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 200, 0, 0, 0, 10, 0, 
                0, 0, 10, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 
                0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 
                0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 
                0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 
                0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 
                0, 0, 30, 0, 0, 0, 30, 0, 0, 0, 30, 0, 0, 0, 15, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 
                0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 50, 0, 0, 0, 50, 0, 
                0, 0, 50, 0, 0, 0, 50, 0, 0, 0, 50, 0, 0, 0, 50, 0, 
                0, 0, 50, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 
                0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 
                0, 0, 1, 0, 0, 0, 0x19, 0, 0, 0, 0x19, 0, 0, 0, 1, 0, 
                0, 0, 1, 0, 0, 0, 0x19, 0, 0, 0, 0x19, 0, 0, 0, 1, 0, 
                0, 0, 0x19, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 
                0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 20, 0, 
                0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 20, 0, 0, 0, 20, 0, 
                0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 
                0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 
                0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 
                0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 
                0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0
             };
                #endregion
                WriteBytes(challenges, UNLOCKCHALLENGES);

                // Emblems
                #region Unlock Emblems
                UNLOCKEMBLEMS = new byte[] { 
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff
             };
                #endregion
                WriteBytes(emblems, UNLOCKEMBLEMS);

                // Titles
                #region Unlock Titles
                UNLOCKTITLES = new byte[64] { 
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff
             };
                #endregion
                WriteBytes(titles, UNLOCKTITLES);

                // Pro Perks
                #region Unlock Perks
                UNLOCKPERKS = new byte[15] { 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7 };
                #endregion
                WriteBytes(PROPERKS, UNLOCKPERKS);

                // Message Box, we'd like to tell our user don't we?
                MessageBox.Show(null, "Everything Unlocked.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(null, "Couldn't unlock everything. Make sure MW3 is running.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (Process_Handle("iw5mp"))
            {
                // Weapons
                WriteBytes(M4A1 + offset, BitConverter.GetBytes(1));
                WriteBytes(M16A4 + offset, BitConverter.GetBytes(1));
                WriteBytes(SCAR + offset, BitConverter.GetBytes(1));
                WriteBytes(CM901 + offset, BitConverter.GetBytes(1));
                WriteBytes(TYPE95 + offset, BitConverter.GetBytes(1));
                WriteBytes(G36C + offset, BitConverter.GetBytes(1));
                WriteBytes(ACR + offset, BitConverter.GetBytes(1));
                WriteBytes(MK14 + offset, BitConverter.GetBytes(1));
                WriteBytes(AK47 + offset, BitConverter.GetBytes(1));
                WriteBytes(FAD + offset, BitConverter.GetBytes(1));
                WriteBytes(MP5 + offset, BitConverter.GetBytes(1));
                WriteBytes(UMP + offset, BitConverter.GetBytes(1));
                WriteBytes(PP90 + offset, BitConverter.GetBytes(1));
                WriteBytes(P90 + offset, BitConverter.GetBytes(1));
                WriteBytes(PM9 + offset, BitConverter.GetBytes(1));
                WriteBytes(MP7 + offset, BitConverter.GetBytes(1));
                WriteBytes(L86 + offset, BitConverter.GetBytes(1));
                WriteBytes(MG36 + offset, BitConverter.GetBytes(1));
                WriteBytes(PKP + offset, BitConverter.GetBytes(1));
                WriteBytes(MK46 + offset, BitConverter.GetBytes(1));
                WriteBytes(M60 + offset, BitConverter.GetBytes(1));
                WriteBytes(BARRET + offset, BitConverter.GetBytes(1));
                WriteBytes(AWP + offset, BitConverter.GetBytes(1));
                WriteBytes(DRAGUNOV + offset, BitConverter.GetBytes(1));
                WriteBytes(AS50 + offset, BitConverter.GetBytes(1));
                WriteBytes(RSASS + offset, BitConverter.GetBytes(1));
                WriteBytes(MSR + offset, BitConverter.GetBytes(1));
                WriteBytes(USAS12 + offset, BitConverter.GetBytes(1));
                WriteBytes(KSG12 + offset, BitConverter.GetBytes(1));
                WriteBytes(SPAS12 + offset, BitConverter.GetBytes(1));
                WriteBytes(AA12 + offset, BitConverter.GetBytes(1));
                WriteBytes(STRIKER + offset, BitConverter.GetBytes(1));
                WriteBytes(M1887 + offset, BitConverter.GetBytes(1));
                WriteBytes(FMG9 + offset, BitConverter.GetBytes(1));
                WriteBytes(SKORPION + offset, BitConverter.GetBytes(1));
                WriteBytes(G18 + offset, BitConverter.GetBytes(1));
                WriteBytes(USP + offset, BitConverter.GetBytes(1));
                WriteBytes(P99 + offset, BitConverter.GetBytes(1));
                WriteBytes(MP412 + offset, BitConverter.GetBytes(1));
                WriteBytes(MAGNUM + offset, BitConverter.GetBytes(1));
                WriteBytes(FIVESEVEN + offset, BitConverter.GetBytes(1));
                WriteBytes(DEAGLE + offset, BitConverter.GetBytes(1));
                WriteBytes(SMAW + offset, BitConverter.GetBytes(1));
                WriteBytes(JAVELIN + offset, BitConverter.GetBytes(1));
                WriteBytes(XM25 + offset, BitConverter.GetBytes(1));
                WriteBytes(M320 + offset, BitConverter.GetBytes(1));
                WriteBytes(RPG + offset, BitConverter.GetBytes(1));
                WriteBytes(SHIELD + offset, BitConverter.GetBytes(1));

                // Prestige
                WriteInteger(PRESTIGE, 0);

                // EXP
                WriteInteger(EXP, 0);

                // Classes
                WriteInteger(CLASSES, 5);

                #region Lock Perks
                LOCKPERKS = new byte[15] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                #endregion
                WriteBytes(PROPERKS, LOCKPERKS);

                // Write Score
                WriteInteger(SCORE, 0);

                // Write Wins
                WriteInteger(WINS, 0);

                // Write Losses
                WriteInteger(LOSSES, 0);

                // Write Kills
                WriteInteger(KILLS, 0);

                // Write Deaths
                WriteInteger(DEATHS, 0);

                // Write Assists
                WriteInteger(ASSISTS, 0);

                // Write Killstreak
                WriteInteger(KILLSTREAK, 0);

                // Write Headshots
                WriteInteger(HEADSHOTS, 0);

                // Write EXP
                WriteInteger(EXP, 0);

                // Write Time
                WriteInteger(TIME, 0);

                // Write Double Weapon XP
                WriteInteger(DOUBLEWEAPONXP, 0);

                // Write Double XP
                WriteInteger(DOUBLEXP, 0);

                // Write Classes
                WriteInteger(CLASSES, 0);

                // Write Prestige
                WriteInteger(PRESTIGE, 0);

                // Write Prestige Tokens
                WriteInteger(TOKENS, 0);

                // Reset Challenges
                #region Reset Challenges
                RESETCHALLENGES = new byte[] { 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
             };
                #endregion
                WriteBytes(challenges, RESETCHALLENGES);

                #region Reset Titles
                RESETTITLES = new byte[] { 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
             };
                #endregion
                WriteBytes(titles, RESETTITLES);

                #region Reset Emblems
                RESETEMBLEMS = new byte[] { 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
             };
                #endregion
                WriteBytes(emblems, RESETEMBLEMS);
                MessageBox.Show(null, "Stats/Weapons/Challenges has been reset.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(null, "Couldn't reset everything. Make sure MW3 is running.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void comboBox4_SelectedValueChanged(object sender, EventArgs e)
        {
            // Change text on button1
            if (comboBox4.Text == "Call of Duty: Modern Warfare")
            {
                button1.Text = "Read Stats From MW";
                groupBox6.Visible = false;
                groupBox5.Visible = false;
            }
            else if (comboBox4.Text == "Call of Duty: Modern Warfare 2")
            {
                button1.Text = "Read Stats From MW2";
                groupBox6.Visible = false;
                groupBox5.Visible = false;
            }
            else if (comboBox4.Text == "Call of Duty: Modern Warfare 3")
            {
                button1.Text = "Read Stats From MW3";
                groupBox6.Visible = false;
                groupBox5.Visible = false;
            }
            else if (comboBox4.Text == "Call of Duty: IW4M (Fourdeltaone)")
            {
                button1.Text = "Read Stats From IW4M";
                groupBox6.Visible = false;
                groupBox5.Visible = false;
            }
            else if (comboBox4.Text == "Call of Duty: Ghosts")
            {
                button1.Text = "Read Stats From Ghosts";
                groupBox6.Visible = true;
                groupBox5.Visible = true;
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (Process_Handle("iw6mp64_ship"))
            {
                System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcessesByName("iw6mp64_ship"); 
                int gamebase = processes[0].MainModule.BaseAddress.ToInt32();

                WriteBytes(gamebase + Convert.ToInt32(0x14432DBCA), BitConverter.GetBytes(0));
            }
            else
            {
                MessageBox.Show("No");
            }
            
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (textBox15.Top == -textBox15.Height)
                textBox15.Top = panel2.Height;
            else
                textBox15.Top -= 1;
            
            
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if (panel4.Top == -panel4.Height)
                panel4.Top = panel3.Height;
            else
                panel4.Top -= 1;
        }
    }

}
