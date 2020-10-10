using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace BDO_Ditto
{
    // Defines blocks of apperance data to copy across
    public struct BdoDataBlock
    {
        public BdoDataBlock(int offset, int length)
        {
            Offset = offset;
            Length = length;
        }

        public int Offset;
        public int Length;
    }

    public class BdoAppearanceSwaper
    {
        private string _sourceApperancePath;
        private string _targetApperancePath;

        private byte[] _sourceApperanceData;
        private byte[] _targetApperanceData;

        public bool LoadSource(string path)
        {
            _sourceApperancePath = path;
            byte[] data = LoadApperance(_sourceApperancePath);
            if (data != null)
            {
                _sourceApperanceData = data;
                return true;
            }
            _sourceApperanceData = null;
            return false;
        }

        public bool LoadTarget(string path)
        {
            _targetApperancePath = path;
            byte[] data = LoadApperance(_targetApperancePath);
            if (data != null)
            {
                _targetApperanceData = data;
                return true;
            }
            _targetApperanceData = null;
            return false;
        }

        public void CopySectionsToTarget(List<BdoDataBlock> setionsToCopy)
        {
            if (_sourceApperanceData != null && _targetApperanceData != null)
            {
                byte[] newTemplate = new byte[_targetApperanceData.Length];
                _targetApperanceData.CopyTo(newTemplate, 0);

                foreach (var section in setionsToCopy)
                {
                    Array.Copy(_sourceApperanceData, section.Offset, newTemplate, section.Offset, section.Length);
                }

                try {
                    File.WriteAllBytes(_targetApperancePath, newTemplate);
                }
                catch (Exception e) {
                    MessageBox.Show(@"Error saving customisation file, sorry :<\n " + e, @"Error Saving");
                }

                var result = MessageBox.Show(@"Sections have been copied to target.   ᕕ( ՞ ᗜ ՞ )ᕗ\nCommit changes and reload?", @"Done", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    LoadTarget(_targetApperancePath);
                }
            }
        }

        // TODO: Check file version
        private byte[] LoadApperance(string path)
        {
            if (File.Exists(path))
            {
                try
                {
                    byte[] data = File.ReadAllBytes(path);
                    uint version = BitConverter.ToUInt32(data, 0);
                    if (StaticData.SuportedVersions.Contains(version))
                    {
                        return data;
                    }
                    string suportedVersionStr = string.Join(", ", StaticData.SuportedVersions);
                    MessageBox.Show(string.Format("Error loading Apperance data\nUnsuported version {0}, only versions {1} are supported, sorry :<\nTry loading it and resaving it in game.", version, suportedVersionStr), @"Error");
                    return null;
                }
                catch (Exception e)
                {
                    MessageBox.Show(@"Error loading Apperance data\n" + e, @"Error");
                    return null;
                }
            }
            MessageBox.Show(@"Apperance file does not exist.\n" + path, @"Error");
            return null;
        }

        private string GetClassFromData(byte[] data)
        {
            // Crude
            ulong classId = BitConverter.ToUInt64(data, StaticData.ClassId.Offset);
            if (!StaticData.ClassIdLookup.TryGetValue(classId, out string className))
            {
                className = "Unknown";
                MessageBox.Show(string.Format("Class ID: {0}, Name: {1}", classId, className), @"Class Unknown", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            Debug.WriteLine("Class ID: {0}, Name: {1}", classId, className);

            return className;
        }

        public string GetSourceClassStr()
        {
            return GetClassFromData(_sourceApperanceData);
        }

        public string GetTargetClassStr()
        {
            return GetClassFromData(_targetApperanceData);
        }

        public bool IsSourceAndTragetApperanceLoaded()
        {
            return _sourceApperanceData != null && _targetApperanceData != null;
        }
    }
}
