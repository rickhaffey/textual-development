import json
import re
from io import open
from bs4 import BeautifulSoup
from StringIO import StringIO
import requests


def get_info(id):

    # request album info by id
    url = "http://www.rollingstone.com/ajax/get-list-items?ids=" + id
    response = requests.get(url)

    html_fragment = response.json()
    html_fragment = html_fragment[0].replace("\/", "/")

    soup = BeautifulSoup(html_fragment)

    # start aggregating the details from the response
    album = {}

    list_item = soup.select("article.list-item")[0]
    
    # parse out album URL and rank
    url_and_rank = list_item['data-bind']
    r1 = re.compile("url:\s*'(.*)'}", re.MULTILINE)
    m1 = r1.search(url_and_rank)
    album['url'] = 'http://www.rollingstone.com' + m1.group(1)
    album['rank'] = list_item.select(".list-item-hd span.long-list-number")[0].text
    
    # parse out artist and title
    heading = list_item.select(".list-item-hd h2")[0].text
    if(heading == "Bob Marley and the Wailers' 'Exodus'"):
        # just handle this one-off
        album['artist'] = 'Bob Marley and the Wailers'
        album['title'] = 'Exodus'
    else:
        r2 = re.compile("(.*)\s*,\s*'(.*)('|$)")
        m2 = r2.search(heading)
        album['artist'] = m2.group(1)
        album['title'] = m2.group(2).strip().rstrip("'")

    # parse out image URL
    album['img_url'] = list_item.select(".img-container img")[0].get('src')
    
    # parse out label and year
    r3 = re.compile("(.*)\s*,\s*(.*)")
    m3 = r3.search(list_item.select(".article-content p em")[0].text)
    if(m3):
        album['label'] = m3.group(1).strip()
        album['year'] = m3.group(2).strip() 

    # parse out summary
    if(m3):
        album['summary'] = list_item.select(".article-content p")[1].text
    else:
        album['summary'] = list_item.select(".article-content p")[0].text
        

    # skip artist URL -- it's not always present so we'll ignore for now...
    #album['artist_url'] = list_item.select(".article-content p")[1].select("a")[0].get("href")

    print(album['rank'] + ': ' + album['artist'] + ' - ' + album['title'])

    # write out to temp .json file
    filepath = 'albums/' + album['rank'] + '.json'
    with open(filepath, 'w') as f:
        f.write(unicode(json.dumps(album)))

    
    
if __name__ == "__main__":
    album_ids = ["145956","145955","145954","145953","145952","145951","145950","145949","145725","145726","145727","145724","145728","145729","145730","145731","145732","145733","145734","145735","145948","145736","145737","145947","145738","145739","145946","145740","145741","145742","145743","145744","145745","145746","145747","145945","145748","145749","145750","145751","145753","145754","145756","145944","145757","145758","145759","145760","145761","145957","145762","145763","145764","145765","145766","145767","145768","145769","145770","145771","145772","145773","145774","145961","145775","145776","145777","145778","145779","145960","145959","145781","145780","145782","145784","145785","145958","145783","145787","145788","145789","145790","145791","145793","145792","145794","145795","145796","145797","145798","145799","145800","145801","145803","145802","145804","145805","145806","145807","145808","145968","145809","145810","145811","145812","145966","145813","145964","146294","145815","145816","145817","145978","145819","145820","145752","145821","145822","145823","145974","145824","145825","145826","145827","145829","145830","145831","145832","145833","146093","145836","145837","145828","145839","145838","145834","145840","145841","145842","145843","145844","145845","146092","146090","146111","146110","146109","146108","146107","146106","146105","146104","146103","146102","146101","146100","146099","146098","146097","146096","146095","146094","146138","146137","146136","146135","146134","146133","146132","146131","146130","146129","146128","146127","146126","146125","146124","146123","146122","146121","146120","146119","146118","146117","146116","146115","146114","146113","146112","146154","146155","145814","146156","145818","146157","146158","146159","146160","146161","146162","146163","146164","146165","146166","146167","146168","146169","146170","146171","146172","146173","146174","146175","146176","146177","146189","146190","146191","146192","146193","146194","146195","146196","146197","146198","146199","146200","146201","146202","146203","146204","146205","146206","146207","146208","146209","146210","146212","146211","146213","146214","146215","146216","146217","146218","146219","146220","146221","145755","146222","146223","146224","146225","146226","146227","146228","146229","146230","146231","146232","146233","146234","146235","146236","146237","146238","146239","146240","146241","146242","146243","146244","146245","146246","146247","146248","146249","146250","146251","146252","146253","146254","146255","146256","146257","146258","146259","146260","146283","146282","146281","146280","146279","146278","146277","146276","146275","146274","146273","146272","146271","145835","146270","146269","146268","146267","146266","146265","146264","146263","146262","146261","146153","146152","146151","146150","146149","146148","146147","146146","146145","146144","146143","146142","146141","146140","146082","146081","146080","146079","146078","146077","146076","146075","146074","146073","146072","146071","146070","146069","146068","146067","146066","146065","146064","146062","146060","146058","146055","146052","146051","146049","146139","146046","146045","146044","146042","146040","146038","146036","146034","146033","146032","146030","146029","146028","146024","146022","146020","146019","146017","146012","146011","146010","146009","146008","146007","146006","146005","146004","146003","146002","146001","146000","145999","145998","145997","145996","145995","145994","145993","145992","145991","145990","145989","145988","145987","145986","145985","145984","145983","145982","145981","145980","145979","145977","145976","145975","145973","145972","145971","145970","145969","145967","145965","145963","145962","145943","145942","145941","145940","145939","145938","145937","145936","145935","145934","145933","145932","145931","145930","145929","145928","145786","145927","145926","145925","145924","145923","145922","145921","145920","145919","145918","145917","145916","145915","145914","145913","145912","145911","145910","145909","145908","145907","145906","145905","145904","145903","145902","145901","145900","145899","145898","145897","145895","145896","145894","145893","145892","145891","145890","145889","145888","145887","145886","145885","145884","145883","145882","145881","145880","145879","145878","145877","145876","145875","145874","145873","145872","145871","145870","145869","145868","145867","145866","145865","145864","145863","145862"]

    for id in album_ids:
        get_info(id)
