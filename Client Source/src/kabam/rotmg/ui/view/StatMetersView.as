package kabam.rotmg.ui.view {
import com.company.assembleegameclient.objects.Player;
import com.company.assembleegameclient.ui.ExperienceBoostTimerPopup;
import com.company.assembleegameclient.ui.StatusBar;

import flash.display.Bitmap;

import flash.display.Sprite;
import flash.events.Event;
import flash.filters.DropShadowFilter;

import kabam.rotmg.assets.BarsOverlay;

import kabam.rotmg.text.model.TextKey;

public class StatMetersView extends Sprite {

    private var expBar_:StatusBar;
    private var fameBar_:StatusBar;
    private var hpBar_:StatusBar;
    private var mpBar_:StatusBar;
    private var areTempXpListenersAdded:Boolean;
    private var curXPBoost:int;
    private var expTimer:ExperienceBoostTimerPopup;
    private var BarOverlay1:Bitmap;
    private var BarOverlay2:Bitmap;
    private var BarOverlay3:Bitmap;

    public function StatMetersView() {
        this.expBar_ = new StatusBar(176, 16, 5931045, 0x545454, TextKey.EXP_BAR_LEVEL);
        this.fameBar_ = new StatusBar(176, 16, 0xE25F00, 0x545454, TextKey.CURRENCY_FAME);
        this.hpBar_ = new StatusBar(176, 16, 14693428, 0x545454, TextKey.STATUS_BAR_HEALTH_POINTS);
        this.mpBar_ = new StatusBar(176, 16, 6325472, 0x545454, TextKey.STATUS_BAR_MANA_POINTS);
        this.hpBar_.y = 24;
        this.mpBar_.y = 48;
        this.expBar_.visible = true;
        this.fameBar_.visible = false;
        addChild(this.expBar_);
        addChild(this.fameBar_);
        addChild(this.hpBar_);
        addChild(this.mpBar_);
     /* this.addBarOverlay1();
        this.addBarOverlay2();
        this.addBarOverlay3(); */
    }

    private function addBarOverlay1():void {
            this.BarOverlay1 = new BarsOverlay();
            this.BarOverlay1.filters = [new DropShadowFilter(0, 0, 0, 0.5, 12, 12)];
            addChild(this.BarOverlay1);
        
    }

    private function addBarOverlay2():void {
        this.BarOverlay2 = new BarsOverlay();
        this.BarOverlay2.filters = [new DropShadowFilter(0, 0, 0, 0.5, 12, 12)];
        this.BarOverlay2.y = 24;
        addChild(this.BarOverlay2);

    }

    private function addBarOverlay3():void {
        this.BarOverlay3 = new BarsOverlay();
        this.BarOverlay3.filters = [new DropShadowFilter(0, 0, 0, 0.5, 12, 12)];
        this.BarOverlay3.y = 48;
        addChild(this.BarOverlay3);

    }

    public function update(_arg_1:Player):void {
        this.expBar_.setLabelText(TextKey.EXP_BAR_LEVEL, {"level": _arg_1.level_});
        if (_arg_1.level_ != 20) {
            if (this.expTimer) {
                this.expTimer.update(_arg_1.xpTimer);
            }
            if (!this.expBar_.visible) {
                this.expBar_.visible = true;
                this.fameBar_.visible = false;
            }
            this.expBar_.draw(_arg_1.exp_, _arg_1.nextLevelExp_, 0);
            if (this.curXPBoost != _arg_1.xpBoost_) {
                this.curXPBoost = _arg_1.xpBoost_;
                if (this.curXPBoost) {
                    this.expBar_.showMultiplierText();
                }
                else {
                    this.expBar_.hideMultiplierText();
                }
            }
            if (_arg_1.xpTimer) {
                if (!this.areTempXpListenersAdded) {
                    this.expBar_.addEventListener("MULTIPLIER_OVER", this.onExpBarOver);
                    this.expBar_.addEventListener("MULTIPLIER_OUT", this.onExpBarOut);
                    this.areTempXpListenersAdded = true;
                }
            }
            else {
                if (this.areTempXpListenersAdded) {
                    this.expBar_.removeEventListener("MULTIPLIER_OVER", this.onExpBarOver);
                    this.expBar_.removeEventListener("MULTIPLIER_OUT", this.onExpBarOut);
                    this.areTempXpListenersAdded = false;
                }
                if (((this.expTimer) && (this.expTimer.parent))) {
                    removeChild(this.expTimer);
                    this.expTimer = null;
                }
            }
        }
        else {
            if (!this.fameBar_.visible) {
                this.fameBar_.visible = true;
                this.expBar_.visible = false;
            }
            this.fameBar_.draw(_arg_1.currFame_, _arg_1.nextClassQuestFame_, 0);
        }
        this.hpBar_.draw(_arg_1.hp_, _arg_1.maxHP_, _arg_1.maxHPBoost_, _arg_1.maxHPMax_);
        this.mpBar_.draw(_arg_1.mp_, _arg_1.maxMP_, _arg_1.maxMPBoost_, _arg_1.maxMPMax_);
    }

    private function onExpBarOver(_arg_1:Event):void {
        addChild((this.expTimer = new ExperienceBoostTimerPopup()));
    }

    private function onExpBarOut(_arg_1:Event):void {
        if (((this.expTimer) && (this.expTimer.parent))) {
            removeChild(this.expTimer);
            this.expTimer = null;
        }
    }


}
}//package kabam.rotmg.ui.view
