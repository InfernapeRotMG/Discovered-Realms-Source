package kabam.rotmg.ui.view {
import kabam.rotmg.account.core.signals.OpenMoneyWindowSignal;
import kabam.rotmg.core.model.PlayerModel;
import kabam.rotmg.dialogs.control.CloseDialogsSignal;

import robotlegs.bender.bundles.mvcs.Mediator;

public class CharacterSlotNeedFameMediator extends Mediator {

    [Inject]
    public var view:CharacterSlotNeedFameDialog;
    [Inject]
    public var closeDialog:CloseDialogsSignal;
    [Inject]
    public var openMoneyWindow:OpenMoneyWindowSignal;
    [Inject]
    public var model:PlayerModel;


    override public function initialize():void {
        this.view.buyFame.add(this.onBuyFame);
        this.view.cancel.add(this.onCancel);
        this.view.setPrice(this.model.getNextCharSlotPrice());
    }

    override public function destroy():void {
        this.view.buyFame.remove(this.onBuyFame);
        this.view.cancel.remove(this.onCancel);
    }

    public function onCancel():void {
        this.closeDialog.dispatch();
    }

    public function onBuyFame():void {
        this.openMoneyWindow.dispatch();
    }


}
}//package kabam.rotmg.ui.view
