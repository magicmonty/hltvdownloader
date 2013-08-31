# Homeload.tv Downloader

[![Build Status](https://travis-ci.org/magicmonty/hltvdownloader.png)](https://travis-ci.org/magicmonty/hltvdownloader)

This is an effort to build a downloader for [HomeloadTv.com](http://www.homeloadtv.com/) based on aria2. 

This project should eventually be able to run cross platform on Linux,Mac and Windows.

This is a work in progress. 

## Programming interface description to HomeloadTV.com                                                                  

### Base-URL

  http://www.homeloadtv.com/api/?

----

<a id="requesturl"></a>
### Request a new URL-List

#### Required
<table>
<tr><th>PARAM</th><th>VALUE</th><th>DESCRIPTION</th></tr>
<tr><td>do</td><td>getlinks</td><td>get the links</td></tr>                              
<tr><td>uid</td><td>%UID%</td><td>the mail adress or user id at homeloadtv.com</td></tr>
<tr><td>password</td><td>%PASSWORD%</td><td>md5 hash of the homeloadtv.com password</td></tr>
<tr><td>limit</td><td>%LIMIT%</td><td>limits the links to receive</td></tr>
</table>

#### Optional

<table>
<tr><th>PARAM</th><th>VALUE</th><th>DESCRIPTION</th></tr>
<tr><td>protocnew</td><td>true</td><td>Should move links from "active" to "new"</td></tr>
<tr><td>onlyhh</td><td>true</td><td>only receive links while it's happy hour</td></tr>
</table>
  
##### Example

    http://www.homeloadtv.com/api/?uid=123456&password=<md5hashofpassword>&limit=30&protocnew=true&do=getlinks

#### Possible Responses

##### Error

<table>
<tr><th>VALUE</th><th>DESCRIPTION</th></tr>
<tr><td>USER_NOT_FOUND</td><td>user id or email adress cannot be found</td></tr>
<tr><td>WRONG_PASSWORD</td><td>wrong password given</td></tr>
<tr><td>NO_NEW_LINKS</td><td>no new links on the server to send</td></tr>
<tr><td>DB_ERROR</td><td>database or server error</td></tr>
<tr><td>NOT_ALLOWED</td><td>only if you try to download from a friend and you're not allowded to do</td></tr>
</table>

##### Success (Pattern) 

<table>
<tr><th>VALUE</th><th>DESCRIPTION</th></tr>
<tr><td>INTERVAL=%INTERVAL%</td><td>the time in minutes to wait before requesting a new list</td></tr>
<tr><td>NUMBER_OF_LINKS=%L_COUNT%</td><td>the number of links</td></tr>
<tr><td>LIST=%LIST_ID%</td><td>running number of the current list</td></tr>
<tr><td>LINKCOUNT=%LINKCOUNT%</td><td>links waiting for download on server</td></tr>
<tr><td>LINKCOUNT=%LINKCOUNT%</td><td>links waiting for download on server</td></tr>
<tr><td>HHSTART=%HHSTART%</td><td>Hour (0-23) when the HappyHour starts</td></tr>
<tr><td>HHEND=%HHEND%</td><td>Hour (0-23) when the HappyHour ends</td></tr>
<tr><td>http://url/file;%LINK_ID%</td><td>download URL;ID of the link</td></tr>
</table>
          
##### Example        
    INTERVAL=15;NUMBER_OF_LINKS=3;LIST=23;LINKCOUNT=137;HHSTART=0;HHEND=8;
    http://10.11.12.9/download/1111111/1/2222222/1234567345abcdefg1235/download1.mpg.avi.otrkey;987645;
    http://10.11.12.9/download/1111112/1/2223222/123abc112fg1bcefg1235/download2.mpg.avi.otrkey;234567;
    http://10.11.12.9/download/1111143/1/2225929/123456789a345abc44d35/download3.mpg.avi.otrkey;345678;

----

### Moving links on the server (PROCESSING)

#### Required    

<table>
<tr><th>PARAM</th><th>VALUE</th><th>DESCRIPTION</th></tr>
<tr><td>do</td><td>setstate</td><td>set the state submited with "state"</td></tr>
<tr><td>uid</td><td>%UID%</td><td>the mail adress or user id at homeloadtv.com</td></tr>
<tr><td>list</td><td>%LIST_ID%</td><td>LIST_ID from the url list response</td></tr>
<tr><td>state</td><td>processing</td><td>moves all the links of the list %LIST_ID% to "processing"</td></tr>
</table>
    
##### Example
    http://www.homeloadtv.com/api/?do=setstate&uid=123456&list=23&state=processing

#### Possible Response

<table>
<tr><th>VALUE</th><th>DESCRIPTION</th></tr>
<tr><td>OK</td><td>okay</td></tr>
</table>      

----

### Moving links on the server (FINISHED / NEW)

#### Required    

<table>
<tr><th>PARAM</th><th>VALUE</th><th>DESCRIPTION</th></tr>
<tr><td>do</td><td>setstate</td><td>set the state submited with "state"</td></tr>
<tr><td>uid</td><td>%UID%</td><td>the mail adress or user id at homeloadtv.com</td></tr>
<tr><td>id</td><td>%LINK_ID%</td><td>the ID of the specific link</td></tr>
<tr><th colspan="3">FINISHED</th></tr>
<tr><td>state</td><td>finished or new</td><td>moves the link to the section "finished" or "new"</td></tr>
<tr><td rowspan="3" style="vertical-align:top;">error</td><td></td><td>leave blank, download was successful</td></tr>
<tr><td>endHH</td><td>Happy-Hour is over</td></tr>
<tr><td>reason</td><td>sets the error-message to "reason"</td></tr>
</table>

#### Optional    

<table>
<tr><th>PARAM</th><th>VALUE</th><th>DESCRIPTION</th></tr>
<tr><td>filesize</td><td>integer</td><td>filesize of the download in KiloByte</td></tr>
<tr><td>speed</td><td>integer</td><td>download speed in kilobit per second</td></tr>
<tr><td>file</td><td>filename</td><td>base64 encoding of the filename -- sends the filename of the dowloaded link to the server. (Currently not used on the server)</td></tr>
</table>
      
##### EXAMPLE
    http://www.homeloadtv.com/api/?do=setstate&uid=123456&state=finished&filesize=9999&id=987645&error=test&speed=7182&file=justAfilename

#### Possible Response

<table>
<tr><th>VALUE</th><th>DESCRIPTION</th></tr>
<tr><td>OK</td><td>okay</td></tr>
</table>    

----

### Download from a friend

#### Request a new URL-List from a friend

- same procedure as on [Request a new URL-List](#requesturl) but you don't have to submit a password
- instead using your own user id for the uid variable use the user id or email adress of your friend you want to download from!

#### Moving links

- same procedure like above but instead using your own user id for the uid variable use the user id or email adress of your friend you want to download from!