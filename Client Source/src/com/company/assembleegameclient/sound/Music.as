package com.company.assembleegameclient.sound {
import com.company.assembleegameclient.parameters.Parameters;

import flash.media.Sound;
import flash.media.SoundChannel;
import flash.media.SoundTransform;
import flash.net.URLRequest;

import kabam.rotmg.application.api.ApplicationSetup;
import kabam.rotmg.core.StaticInjectorContext;

public class Music {

    public static const menuMusic:Vector.<String> = new <String>["Menu", "Menu", "Menu"];
    public static var fadeIn:Number = 0.65;
    public static var fadeOut:Number = 0;
    public static var music_:String = "Menu";
    private static var currentSoundChannel:SoundChannel = null;
    private static var currentSoundTransform:SoundTransform = null;
    private static var newSound:Sound = null;
    private static var newSoundChannel:SoundChannel = null;
    private static var newSoundTransform:SoundTransform = null;
    private static var volume:Number = Parameters.data_.musicVolume;


    public static function load():void {
        var currentSound:Sound = new Sound();
        currentSound.load(new URLRequest((("http://" + Parameters.musicUrl_) + "/sfx/music/" + randomMenu() + ".mp3")));
        currentSoundTransform = new SoundTransform(Parameters.data_.playMusic ? 0.65 : 0);
        currentSoundChannel = currentSound.play(0, int.MAX_VALUE, currentSoundTransform);
    }

    public static function setPlayMusic(param1:Boolean) : void {
        Parameters.data_.playMusic = param1;
        Parameters.save();
        newSoundTransform.volume = !!Parameters.data_.playMusic?Number(volume):Number(0);
        newSoundChannel.soundTransform = newSoundTransform;
    }

    public static function _continue(_arg1:Boolean):void {
        Parameters.data_.playMusic = _arg1;
        Parameters.save();
        currentSoundChannel.soundTransform = new SoundTransform(((Parameters.data_.playMusic) ? 0.65 : 0));
    }

    public static function randomMenu():String {
        return menuMusic[Math.round(Math.random() * (menuMusic.length - 1))];
    }

    public static function reload(name:String):void {
        if (music_ == name) return;
        music_ = name;
        try {
            if (fadeIn < 0.65 && newSoundChannel != null) {
                endFade();
            }
            newSound = new Sound();
            newSound.load(new URLRequest("http://" + Parameters.musicUrl_ + "/sfx/music/" + (name == "Menu" ? randomMenu() : name) + ".mp3"));
            newSoundTransform = new SoundTransform(Parameters.data_.playMusic ? 0.65 : 0);
            newSoundChannel = newSound.play(0, int.MAX_VALUE, newSoundTransform);
        } catch (e:Error) {
            trace(e);
        }
        //if (fadeIn < 0.65) endFade();
        fadeIn = 0;
        fadeOut = 0.65;
    }

    public static function endFade():void {
        try {
            currentSoundChannel.stop();
        } catch (er:Error)
        {
            trace(er);
        }
        currentSoundChannel = newSoundChannel;
        currentSoundTransform = newSoundTransform;

        currentSoundTransform.volume = Parameters.data_.playMusic ? 0.65 : 0;
        fadeIn = 0.65;
        fadeOut = 0;
    }

    public static function updateFade():void {
        if (fadeIn >= 0.65) return;
        if (!Parameters.data_.playMusic) {
            endFade();
            return;
        }
        try {
            fadeIn += 0.0065;
            fadeOut -= 0.0065;
            if(fadeOut < 0) {
                fadeOut = 0;
            }
            currentSoundTransform.volume = fadeOut;
            newSoundTransform.volume = fadeIn;
            currentSoundChannel.soundTransform = currentSoundTransform;
            newSoundChannel.soundTransform = newSoundTransform;
        } catch (e:Error) {
            trace(e);
        }
        if (fadeIn >= 0.65) endFade();
    }

    //Volume Scroll
    public static function setMusicVolume(_arg_1:Number):void {
        Parameters.data_.musicVolume = _arg_1;
        Parameters.save();
        if (!Parameters.data_.playMusic) {
            return;
        }
        if (newSoundTransform != null) {
            newSoundTransform.volume = _arg_1;
        }
        else {
            newSoundTransform = new SoundTransform(_arg_1);
        }
        newSoundChannel.soundTransform = newSoundTransform;
    }

}
}//package com.company.assembleegameclient.sound
