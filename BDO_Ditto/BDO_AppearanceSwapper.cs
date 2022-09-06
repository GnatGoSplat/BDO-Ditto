using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace BDO_Ditto
{
    // Defines blocks of appearance data to copy across
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

    public class BdoAppearanceSwapper
    {
        private string _sourceAppearancePath;
        private string _targetAppearancePath;

        private byte[] _sourceAppearanceData;
        private byte[] _targetAppearanceData;

        private uint _sourceVersion;
        private uint _targetVersion;

        public bool LoadSource(string path)
        {
            _sourceAppearancePath = path;
            byte[] data = LoadAppearance(_sourceAppearancePath);
            if (data != null)
            {
                _sourceVersion = BitConverter.ToUInt32(data, 0);
                _sourceAppearanceData = data;
                return true;
            }
            _sourceAppearanceData = null;
            return false;
        }

        public bool LoadTarget(string path)
        {
            _targetAppearancePath = path;
            byte[] data = LoadAppearance(_targetAppearancePath);
            if (data != null)
            {
                _targetVersion = BitConverter.ToUInt32(data, 0);
                _targetAppearanceData = data;
                return true;
            }
            _targetAppearanceData = null;
            return false;
        }

        public void CopySectionsToTarget(List<string> sectionsToCopy)
        {
            if (_sourceAppearanceData != null && _targetAppearanceData != null)
            {
                byte[] newTemplate = new byte[_targetAppearanceData.Length];
                bool proceed = false;
                _targetAppearanceData.CopyTo(newTemplate, 0);

                foreach (var section in sectionsToCopy)
                {
                    BdoDataBlock sourceBlock = (_sourceVersion <= 19) ? StaticData.AppearanceSectionsOld[section] : StaticData.AppearanceSections[section];
                    BdoDataBlock targetBlock = (_targetVersion <= 19) ? StaticData.AppearanceSectionsOld[section] : StaticData.AppearanceSections[section];
                    Debug.WriteLine("source start offset: " + sourceBlock.Offset);
                    Debug.WriteLine("target start offset: " + targetBlock.Offset + " length: " + targetBlock.Length);

                    Array.Copy(_sourceAppearanceData, sourceBlock.Offset, newTemplate, targetBlock.Offset, targetBlock.Length);
                }

                if ((_sourceVersion <= 19 && _targetVersion >= 20) || (_sourceVersion >= 20 && _targetVersion <= 19))
                {
                    DialogResult versionMismatch = MessageBox.Show("Source and target versions are incompatible.\n" +
                        "Target customization may crash the game if loaded.\n\n" +
                        "Re-save " + ((_sourceVersion < _targetVersion) ? "source" : "target") + " customization in game to avoid this problem.\n\n" +
                        "Continue anyway?", "Incompatible Versions", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    proceed = (versionMismatch == DialogResult.Yes);
                } else
                {
                    proceed = true;
                }
                
                if (proceed) { 
                    try {
                        File.WriteAllBytes(_targetAppearancePath, newTemplate);
                    }
                    catch (Exception e) {
                        MessageBox.Show("Error saving customisation file, sorry :<\n " + e, "Error Saving");
                    }

                    var result = MessageBox.Show("Sections have been copied to target.   ᕕ( ՞ ᗜ ՞ )ᕗ\nCommit changes and reload?", "Done", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        LoadTarget(_targetAppearancePath);
                    }
                }
            }
        }

        // TODO: Check file version
        private byte[] LoadAppearance(string path)
        {
            if (File.Exists(path))
            {
                try
                {
                    byte[] data = File.ReadAllBytes(path);
                    uint version = BitConverter.ToUInt32(data, 0);
                    if (StaticData.SupportedVersions.Contains(version))
                    {
                        return data;
                    }
                    string supportedVersionStr = string.Join(", ", StaticData.SupportedVersions);
                    MessageBox.Show(string.Format("WARNING\nUnsupported version {0}, only versions {1} are supported.  Results may be unexpected.", version, supportedVersionStr), @"Error");
                    return data;
                }
                catch (Exception e)
                {
                    MessageBox.Show(@"Error loading Appearance data\n" + e, @"Error");
                    return null;
                }
            }
            MessageBox.Show(@"Appearance file does not exist.\n" + path, @"Error");
            return null;
        }

        private string GetClassFromData(byte[] data)
        {
            // Crude
            uint version = BitConverter.ToUInt32(data, 0);
            ulong classId = (version <= 19) ? BitConverter.ToUInt64(data, StaticData.ClassIdOld.Offset) : BitConverter.ToUInt64(data, StaticData.ClassId.Offset);
            if (!StaticData.ClassIdLookup.TryGetValue(classId, out string className))
            {
                className = "Unknown";
                MessageBox.Show(string.Format("Class ID: {0}, Name: {1}", classId, className), @"Class Unknown", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            Debug.WriteLine("Class ID: {0}, Name: {1}", classId, className);

            return className + " v" + version;
        }

        public string GetSourceClassStr()
        {
            return GetClassFromData(_sourceAppearanceData);
        }

        public string GetTargetClassStr()
        {
            return GetClassFromData(_targetAppearanceData);
        }

        public bool IsSourceAndTragetAppearanceLoaded()
        {
            return _sourceAppearanceData != null && _targetAppearanceData != null;
        }
    }
}
