
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//Generated on: 18.02.2021 21:45.52
public class SettingsConstants
{

    public enum Name
    {
		MUSIC_VOLUME,
		TERRAIN_GENERATION_PIXELS_PER_UNIT,
		SOUND_VOLUME,
		TIME_CONTROL_ELEMENTS_SIZE,
		TIME_CONTROL_CHANGE_DIFFERENCE,

    }

    public static void Load()
    {
		SettingsController.Instance.AddSetting(new SettingValue()
		{
			Name = Enum.GetName(typeof(SettingsConstants.Name), Name.MUSIC_VOLUME),
			Type = SettingValueType.Float,
			MinValue = "0.0f",
			DefaultValue = "0.5f",
			MaxValue = "1.0f"
		});
		SettingsController.Instance.AddSetting(new SettingValue()
		{
			Name = Enum.GetName(typeof(SettingsConstants.Name), Name.TERRAIN_GENERATION_PIXELS_PER_UNIT),
			Type = SettingValueType.Integer,
			MinValue = "0",
			DefaultValue = "256",
			MaxValue = "1024"
		});
		SettingsController.Instance.AddSetting(new SettingValue()
		{
			Name = Enum.GetName(typeof(SettingsConstants.Name), Name.SOUND_VOLUME),
			Type = SettingValueType.Float,
			MinValue = "0.0f",
			DefaultValue = "0.7f",
			MaxValue = "1.0f"
		});
		SettingsController.Instance.AddSetting(new SettingValue()
		{
			Name = Enum.GetName(typeof(SettingsConstants.Name), Name.TIME_CONTROL_ELEMENTS_SIZE),
			Type = SettingValueType.Integer,
			MinValue = "0",
			DefaultValue = "100",
			MaxValue = "10000"
		});
		SettingsController.Instance.AddSetting(new SettingValue()
		{
			Name = Enum.GetName(typeof(SettingsConstants.Name), Name.TIME_CONTROL_CHANGE_DIFFERENCE),
			Type = SettingValueType.Float,
			MinValue = "0",
			DefaultValue = "0,2",
			MaxValue = "1"
		});

    }
}
