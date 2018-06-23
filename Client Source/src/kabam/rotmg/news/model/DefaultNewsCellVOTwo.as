package kabam.rotmg.news.model {
public class DefaultNewsCellVOTwo extends NewsCellVO {

    public function DefaultNewsCellVOTwo(_arg_1:int) {
        imageURL = "";
        linkDetail = "COMING SOON!";
        headline = (((_arg_1 == 0)) ? "Coming soon!" : "Coming soon!");
        startDate = (new Date().getTime() - 0x3B9ACA00);
        endDate = (new Date().getTime() + 0x3B9ACA00);
        networks = ["kabam.com", "kongregate", "steam", "rotmg"];
        linkType = NewsCellLinkType.OPENS_LINK;
        priority = 999999;
        slot = _arg_1;
    }

}
}//package kabam.rotmg.news.model
