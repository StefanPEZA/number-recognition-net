ÈZ
~D:\number-recognition-net\number-recognition-api\NumberRecognitionAPI\NumberRecognitionAPI\Controllers\V1\DatasetController.cs
	namespace		 	 
NumberRecognitionAPI		
 
.		 
Controllers		 *
.		* +
V1		+ -
{

 
[ 

ApiVersion 
( 
$str 
) 
] 
[ 
Route 

(
 
$str .
). /
]/ 0
[ 
ApiController 
] 
public 

class 
DatasetController "
:# $
ControllerBase% 3
{ 
private 
readonly 
IDatasetService (
_datasetService) 8
;8 9
public 
DatasetController  
(  !
IDatasetService! 0
datasetService1 ?
)? @
{ 	
_datasetService 
= 
datasetService ,
;, -
} 	
[ 	
HttpGet	 
( 
$str 
) 
] 
public 
async 
Task 
< 
IActionResult '
>' (
GetDatasetWithId) 9
(9 :
[: ;
	FromRoute; D
]D E
GuidF J
idK M
)M N
{ 	
object 
response 
; 
var 
datasetEntity 
= 
await  %
_datasetService& 5
.5 6
GetDatasetAsync6 E
(E F
idF H
)H I
;I J
if 
( 
datasetEntity 
==  
null! %
)% &
{ 
response 
= 
new 
{ 
status   
=   
$str   $
,  $ %
message!! 
=!! 
$str!! D
+!!E F
id!!G I
.!!I J
ToString!!J R
(!!R S
)!!S T
}"" 
;"" 
return## 

BadRequest## !
(##! "
response##" *
)##* +
;##+ ,
}$$ 
return%% 
Ok%% 
(%% 
datasetEntity%% #
)%%# $
;%%$ %
}&& 	
[(( 	
HttpGet((	 
((( 
$str(( 
)(( 
]((  
public)) 
async)) 
Task)) 
<)) 
IActionResult)) '
>))' (
GetAllDataset))) 6
())6 7
string))7 =
label))> C
,))C D
[))E F
	FromQuery))F O
]))O P
int))Q T
limit))U Z
=))[ \
$num))] _
)))_ `
{** 	
object++ 
response++ 
;++ 
List,, 
<,, 
Dataset,, 
>,, 
dataset,, !
=,," #
(,,$ %
List,,% )
<,,) *
Dataset,,* 1
>,,1 2
),,2 3
await,,3 8
_datasetService,,9 H
.,,H I
GetAllDatasetAsync,,I [
(,,[ \
label,,\ a
,,,a b
limit,,c h
),,h i
;,,i j
if-- 
(-- 
dataset-- 
==-- 
null-- 
||--  "
dataset--# *
.--* +
Count--+ 0
<=--1 3
$num--4 5
)--5 6
{.. 
response// 
=// 
new// 
{00 
status11 
=11 
$str11 $
,11$ %
message22 
=22 
$str22 @
+22A B
label22C H
.22H I
ToString22I Q
(22Q R
)22R S
}33 
;33 
return44 

BadRequest44 !
(44! "
response44" *
)44* +
;44+ ,
}55 
return66 
Ok66 
(66 
dataset66 
)66 
;66 
}77 	
[99 	
HttpGet99	 
(99 
$str99  
)99  !
]99! "
public:: 
async:: 
Task:: 
<:: 
IActionResult:: '
>::' (
GetAllTrainDataset::) ;
(::; <
string::< B
label::C H
,::H I
[::J K
	FromQuery::K T
]::T U
int::V Y
limit::Z _
=::` a
$num::b d
)::d e
{;; 	
object<< 
response<< 
;<< 
List== 
<== 
Dataset== 
>== 
dataset== !
===" #
(==$ %
List==% )
<==) *
Dataset==* 1
>==1 2
)==2 3
await==3 8
_datasetService==9 H
.==H I#
GetAllTrainDatasetAsync==I `
(==` a
label==a f
,==f g
limit==h m
)==m n
;==n o
if>> 
(>> 
dataset>> 
==>> 
null>> 
||>>  "
dataset>># *
.>>* +
Count>>+ 0
<=>>1 3
$num>>4 5
)>>5 6
{?? 
response@@ 
=@@ 
new@@ 
{AA 
statusBB 
=BB 
$strBB $
,BB$ %
messageCC 
=CC 
$strCC F
+CCG H
labelCCI N
.CCN O
ToStringCCO W
(CCW X
)CCX Y
}DD 
;DD 
returnEE 

BadRequestEE !
(EE! "
responseEE" *
)EE* +
;EE+ ,
}FF 
returnGG 
OkGG 
(GG 
datasetGG 
)GG 
;GG 
}HH 	
[JJ 	
HttpGetJJ	 
(JJ 
$strJJ 
)JJ  
]JJ  !
publicKK 
asyncKK 
TaskKK 
<KK 
IActionResultKK '
>KK' (
GetAllTestDatasetKK) :
(KK: ;
stringKK; A
labelKKB G
,KKG H
[KKI J
	FromQueryKKJ S
]KKS T
intKKU X
limitKKY ^
=KK_ `
$numKKa c
)KKc d
{LL 	
objectMM 
responseMM 
;MM 
ListNN 
<NN 
DatasetNN 
>NN 
datasetNN !
=NN" #
(NN$ %
ListNN% )
<NN) *
DatasetNN* 1
>NN1 2
)NN2 3
awaitNN3 8
_datasetServiceNN9 H
.NNH I"
GetAllTestDatasetAsyncNNI _
(NN_ `
labelNN` e
,NNe f
limitNNg l
)NNl m
;NNm n
ifOO 
(OO 
datasetOO 
==OO 
nullOO 
||OO  "
datasetOO# *
.OO* +
CountOO+ 0
<=OO1 3
$numOO4 5
)OO5 6
{PP 
responseQQ 
=QQ 
newQQ 
{RR 
statusSS 
=SS 
$strSS $
,SS$ %
messageTT 
=TT 
$strTT E
+TTF G
labelTTH M
.TTM N
ToStringTTN V
(TTV W
)TTW X
}UU 
;UU 
returnVV 

BadRequestVV !
(VV! "
responseVV" *
)VV* +
;VV+ ,
}WW 
returnXX 
OkXX 
(XX 
datasetXX 
)XX 
;XX 
}YY 	
private[[ 
async[[ 
Task[[ 
<[[ 
Dataset[[ "
>[[" #
AddToDataset[[$ 0
([[0 1
bool[[1 5
isTest[[6 <
,[[< =
string[[> D
label[[E J
,[[J K
byte[[L P
[[[P Q
][[Q R
image_bytes[[S ^
)[[^ _
{\\ 	
Dataset]] 
dataset]] 
=]] 
new]] !
Dataset]]" )
(]]) *
)]]* +
{^^ 
Label__ 
=__ 
label__ 
,__ 
ImageMatrix`` 
=`` 
image_bytes`` )
,``) *
IsTestaa 
=aa 
isTestaa 
}bb 
;bb 
awaitcc 
_datasetServicecc !
.cc! "
InsertIntoDatasetcc" 3
(cc3 4
datasetcc4 ;
)cc; <
;cc< =
returndd 
datasetdd 
;dd 
}ee 	
[gg 	
HttpPostgg	 
(gg 
$strgg  
)gg  !
]gg! "
publichh 
asynchh 
Taskhh 
<hh 
IActionResulthh '
>hh' (
AddTestDatasethh) 7
(hh7 8
stringhh8 >
labelhh? D
,hhD E
	IFormFilehhF O
imagehhP U
)hhU V
{ii 	
objectjj 
responsejj 
;jj 
boolkk 
validkk 
;kk 
(ll 
validll 
,ll 
responsell 
)ll 
=ll 
Sharedll  &
.ll& '
CheckIfIsValidImagell' :
(ll: ;
imagell; @
)ll@ A
;llA B
ifmm 
(mm 
!mm 
validmm 
)mm 
{nn 
returnoo 

BadRequestoo !
(oo! "
responseoo" *
)oo* +
;oo+ ,
}pp 
byteqq 
[qq 
]qq 
image_bytesqq 
=qq  
awaitqq! &
Sharedqq' -
.qq- . 
IFormFileToByteArrayqq. B
(qqB C
imageqqC H
)qqH I
;qqI J
Datasetrr 
datasetrr 
=rr 
awaitrr #
AddToDatasetrr$ 0
(rr0 1
truerr1 5
,rr5 6
labelrr7 <
,rr< =
image_bytesrr> I
)rrI J
;rrJ K
returnss 
Okss 
(ss 
datasetss 
)ss 
;ss 
}tt 	
[vv 	
HttpPostvv	 
(vv 
$strvv !
)vv! "
]vv" #
publicww 
asyncww 
Taskww 
<ww 
IActionResultww '
>ww' (
AddTrainDatasetww) 8
(ww8 9
stringww9 ?
labelww@ E
,wwE F
	IFormFilewwG P
imagewwQ V
)wwV W
{xx 	
objectyy 
responseyy 
;yy 
boolzz 
validzz 
;zz 
({{ 
valid{{ 
,{{ 
response{{ 
){{ 
={{ 
Shared{{  &
.{{& '
CheckIfIsValidImage{{' :
({{: ;
image{{; @
){{@ A
;{{A B
if|| 
(|| 
!|| 
valid|| 
)|| 
{}} 
return~~ 

BadRequest~~ !
(~~! "
response~~" *
)~~* +
;~~+ ,
} 
byte
ÄÄ 
[
ÄÄ 
]
ÄÄ 
image_bytes
ÄÄ 
=
ÄÄ  
await
ÄÄ! &
Shared
ÄÄ' -
.
ÄÄ- ."
IFormFileToByteArray
ÄÄ. B
(
ÄÄB C
image
ÄÄC H
)
ÄÄH I
;
ÄÄI J
Dataset
ÅÅ 
dataset
ÅÅ 
=
ÅÅ 
await
ÅÅ #
AddToDataset
ÅÅ$ 0
(
ÅÅ0 1
false
ÅÅ1 6
,
ÅÅ6 7
label
ÅÅ8 =
,
ÅÅ= >
image_bytes
ÅÅ? J
)
ÅÅJ K
;
ÅÅK L
return
ÇÇ 
Ok
ÇÇ 
(
ÇÇ 
dataset
ÇÇ 
)
ÇÇ 
;
ÇÇ 
}
ÉÉ 	
}
ÑÑ 
}ÖÖ …B
|D:\number-recognition-net\number-recognition-api\NumberRecognitionAPI\NumberRecognitionAPI\Controllers\V1\ImageController.cs
	namespace 	 
NumberRecognitionAPI
 
. 
Controllers *
.* +
V1+ -
{		 
[

 

ApiVersion

 
(

 
$str

 
)

 
]

 
[ 
Route 

(
 
$str ,
), -
]- .
[ 
ApiController 
] 
public 

class 
ImageController  
:! "
ControllerBase# 1
{ 
private 
readonly 
IImageService &
_imageService' 4
;4 5
public 
ImageController 
( 
IImageService ,
imageService- 9
)9 :
{ 	
_imageService 
= 
imageService (
;( )
} 	
[ 	
HttpPost	 
] 
[ 	
Route	 
( 
$str 
) 
] 
public 
async 
Task 
< 
IActionResult '
>' (
PredictImage) 5
(5 6
	IFormFile6 ?
image@ E
)E F
{ 	
object 
response 
; 
bool 
valid 
; 
( 
valid 
, 
response 
) 
= 
Shared  &
.& '
CheckIfIsValidImage' :
(: ;
image; @
)@ A
;A B
if 
( 
! 
valid 
) 
{   
return!! 

BadRequest!! !
(!!! "
response!!" *
)!!* +
;!!+ ,
}"" 
byte$$ 
[$$ 
]$$ 
image_bytes$$ 
=$$  
await$$! &
Shared$$' -
.$$- . 
IFormFileToByteArray$$. B
($$B C
image$$C H
)$$H I
;$$I J
response&& 
=&& 
new&& 
{'' 
status(( 
=(( 
$str(( 
,(( 
image_length)) 
=)) 
image)) $
.))$ %
Length))% +
,))+ ,
	file_name** 
=** 
image** !
.**! "
FileName**" *
,*** +
	file_type++ 
=++ 
image++ !
.++! "
ContentType++" -
,++- .
predicted_label,, 
=,,  !
$str,," %
,,,% &
}-- 
;-- 
return.. 
Ok.. 
(.. 
response.. 
).. 
;..  
}// 	
[11 	
HttpPost11	 
]11 
[22 	
Route22	 
(22 
$str22 
)22 
]22 
public33 
async33 
Task33 
<33 
IActionResult33 '
>33' (
ResizeImage33) 4
(334 5
	IFormFile335 >
image33? D
,33D E
int33F I
width33J O
,33O P
int33Q T
height33U [
)33[ \
{44 	
object55 
response55 
;55 
bool66 
valid66 
;66 
(77 
valid77 
,77 
response77 
)77 
=77 
Shared77  &
.77& '
CheckIfIsValidImage77' :
(77: ;
image77; @
)77@ A
;77A B
if88 
(88 
!88 
valid88 
)88 
{99 
return:: 

BadRequest:: !
(::! "
response::" *
)::* +
;::+ ,
};; 
byte== 
[== 
]== 
image_bytes== 
===  
await==! &
Shared==' -
.==- . 
IFormFileToByteArray==. B
(==B C
image==C H
)==H I
;==I J
byte>> 
[>> 
]>> 
processed_image>> "
=>># $
await>>% *
_imageService>>+ 8
.>>8 9
Resize>>9 ?
(>>? @
image_bytes>>@ K
,>>K L
width>>L Q
,>>Q R
height>>R X
)>>X Y
;>>Y Z
response@@ 
=@@ 
new@@ 
{AA 
statusBB 
=BB 
$strBB 
,BB 
processed_imageCC 
=CC  !
processed_imageCC" 1
}DD 
;DD 
returnEE 
OkEE 
(EE 
responseEE 
)EE 
;EE  
}FF 	
[HH 	
HttpPostHH	 
]HH 
[II 	
RouteII	 
(II 
$strII 
)II 
]II 
publicJJ 
asyncJJ 
TaskJJ 
<JJ 
IActionResultJJ '
>JJ' (
	CropImageJJ) 2
(JJ2 3
	IFormFileJJ3 <
imageJJ= B
)JJB C
{KK 	
objectLL 
responseLL 
;LL 
boolMM 
validMM 
;MM 
(NN 
validNN 
,NN 
responseNN 
)NN 
=NN 
SharedNN  &
.NN& '
CheckIfIsValidImageNN' :
(NN: ;
imageNN; @
)NN@ A
;NNA B
ifOO 
(OO 
!OO 
validOO 
)OO 
{PP 
returnQQ 

BadRequestQQ !
(QQ! "
responseQQ" *
)QQ* +
;QQ+ ,
}RR 
byteTT 
[TT 
]TT 
image_bytesTT 
=TT  
awaitTT! &
SharedTT' -
.TT- . 
IFormFileToByteArrayTT. B
(TTB C
imageTTC H
)TTH I
;TTI J
byteUU 
[UU 
]UU 
processed_imageUU "
=UU# $
awaitUU% *
_imageServiceUU+ 8
.UU8 9
CropUU9 =
(UU= >
image_bytesUU> I
)UUI J
;UUJ K
responseWW 
=WW 
newWW 
{XX 
statusYY 
=YY 
$strYY 
,YY 
processed_imageZZ 
=ZZ  !
processed_imageZZ" 1
}[[ 
;[[ 
return\\ 
Ok\\ 
(\\ 
response\\ 
)\\ 
;\\  
}]] 	
[__ 	
HttpPost__	 
]__ 
[`` 	
Route``	 
(`` 
$str`` 
)`` 
]`` 
publicaa 
asyncaa 
Taskaa 
<aa 
IActionResultaa '
>aa' (

SplitImageaa) 3
(aa3 4
	IFormFileaa4 =
imageaa> C
)aaC D
{bb 	
objectcc 
responsecc 
;cc 
booldd 
validdd 
;dd 
(ee 
validee 
,ee 
responseee 
)ee 
=ee 
Sharedee  &
.ee& '
CheckIfIsValidImageee' :
(ee: ;
imageee; @
)ee@ A
;eeA B
ifff 
(ff 
!ff 
validff 
)ff 
{gg 
returnhh 

BadRequesthh !
(hh! "
responsehh" *
)hh* +
;hh+ ,
}ii 
bytekk 
[kk 
]kk 
image_byteskk 
=kk  
awaitkk! &
Sharedkk' -
.kk- . 
IFormFileToByteArraykk. B
(kkB C
imagekkC H
)kkH I
;kkI J
Listll 
<ll 
bytell 
[ll 
]ll 
>ll 
processed_imagell (
=ll) *
awaitll+ 0
_imageServicell1 >
.ll> ?
Splitll? D
(llD E
image_bytesllE P
)llP Q
;llQ R
responsenn 
=nn 
newnn 
{oo 
statuspp 
=pp 
$strpp 
,pp 
processed_imageqq 
=qq  !
processed_imageqq" 1
.qq1 2
ToArrayqq2 9
(qq9 :
)qq: ;
}rr 
;rr 
returnss 
Okss 
(ss 
responsess 
)ss 
;ss  
}tt 	
}vv 
}ww Ú
eD:\number-recognition-net\number-recognition-api\NumberRecognitionAPI\NumberRecognitionAPI\Program.cs
	namespace

 	 
NumberRecognitionAPI


 
{ 
public 

static 
class 
Program 
{ 
public 
static 
void 
Main 
(  
string  &
[& '
]' (
args) -
)- .
{ 	
CreateHostBuilder 
( 
args "
)" #
.# $
Build$ )
() *
)* +
.+ ,
Run, /
(/ 0
)0 1
;1 2
} 	
public 
static 
IHostBuilder "
CreateHostBuilder# 4
(4 5
string5 ;
[; <
]< =
args> B
)B C
=>D F
Host 
.  
CreateDefaultBuilder %
(% &
args& *
)* +
. $
ConfigureWebHostDefaults )
() *

webBuilder* 4
=>5 7
{ 

webBuilder 
. 
ConfigureKestrel /
(/ 0
opt0 3
=>4 6
opt7 :
.: ;
Limits; A
.A B
MaxRequestBodySizeB T
=U V
nullW [
)[ \
;\ ]

webBuilder 
. 

UseStartup )
<) *
Startup* 1
>1 2
(2 3
)3 4
;4 5
} 
) 
; 
} 
} ˘*
eD:\number-recognition-net\number-recognition-api\NumberRecognitionAPI\NumberRecognitionAPI\Startup.cs
	namespace 	 
NumberRecognitionAPI
 
{ 
public 

class 
Startup 
{ 
public 
Startup 
( 
IConfiguration %
configuration& 3
)3 4
{ 	
Configuration 
= 
configuration )
;) *
} 	
public 
IConfiguration 
Configuration +
{, -
get. 1
;1 2
}3 4
public 
void 
ConfigureServices %
(% &
IServiceCollection& 8
services9 A
)A B
{ 	
services 
. 
AddCors 
( 
options $
=>% '
{ 
options 
. 
	AddPolicy !
(! "
name" &
:& '
$str( .
,. /
builder" )
=>* ,
{" #
builder  & -
.  - .
WithOrigins  . 9
(  9 :
$str  : =
)  = >
;  > ?
}!!" #
)!!# $
;!!$ %
}"" 
)"" 
;"" 
services## 
.## 
AddControllers## #
(### $
)##$ %
;##% &
services$$ 
.$$ 
AddApiVersioning$$ %
($$% &
options$$& -
=>$$. 0
{%% 
options&& 
.&& /
#AssumeDefaultVersionWhenUnspecified&& ;
=&&< =
true&&> B
;&&B C
options'' 
.'' 
DefaultApiVersion'' )
=''* +
new'', /
	Microsoft''0 9
.''9 :

AspNetCore'': D
.''D E
Mvc''E H
.''H I

ApiVersion''I S
(''S T
$num''T U
,''U V
$num''W X
)''X Y
;''Y Z
options(( 
.(( 
ReportApiVersions(( )
=((* +
true((, 0
;((0 1
})) 
))) 
;)) 
services** 
.** 
AddSwaggerGen** "
(**" #
c**# $
=>**% '
{++ 
c,, 
.,, 

SwaggerDoc,, 
(,, 
$str,, !
,,,! "
new,,# &
OpenApiInfo,,' 2
{,,3 4
Title,,5 :
=,,; <
$str,,= S
,,,S T
Version,,U \
=,,] ^
$str,,_ c
},,d e
),,e f
;,,f g
}-- 
)-- 
;-- 
services.. 
... 
AddDbContext.. !
<..! " 
ApplicationDbContext.." 6
>..6 7
(..7 8
item..8 <
=>..= ?
item..@ D
...D E
	UseSqlite..E N
(..N O
Configuration..O \
...\ ]
GetConnectionString..] p
(..p q
$str..q }
)..} ~
)..~ 
)	.. Ä
;
..Ä Å
services// 
.// 
	AddScoped// 
(// 
typeof// %
(//% &
IRepository//& 1
<//1 2
>//2 3
)//3 4
,//4 5
typeof//6 <
(//< =

Repository//= G
<//G H
>//H I
)//I J
)//J K
;//K L
services00 
.00 
AddTransient00 !
<00! "
IDatasetService00" 1
,001 2
DatasetService003 A
>00A B
(00B C
)00C D
;00D E
services11 
.11 
AddTransient11 !
<11! "
IImageService11" /
,11/ 0
ImageService111 =
>11= >
(11> ?
)11? @
;11@ A
}22 	
public55 
void55 
	Configure55 
(55 
IApplicationBuilder55 1
app552 5
,555 6
IWebHostEnvironment557 J
env55K N
)55N O
{66 	
if77 
(77 
env77 
.77 
IsDevelopment77 !
(77! "
)77" #
)77# $
{88 
app99 
.99 %
UseDeveloperExceptionPage99 -
(99- .
)99. /
;99/ 0
app:: 
.:: 

UseSwagger:: 
(:: 
)::  
;::  !
app;; 
.;; 
UseSwaggerUI;;  
(;;  !
c;;! "
=>;;# %
c;;& '
.;;' (
SwaggerEndpoint;;( 7
(;;7 8
$str;;8 R
,;;R S
$str;;T m
);;m n
);;n o
;;;o p
}<< 
app>> 
.>> 
UseCors>> 
(>> 
$str>> 
)>> 
;>>  
app@@ 
.@@ 
UseHttpsRedirection@@ #
(@@# $
)@@$ %
;@@% &
appBB 
.BB 

UseRoutingBB 
(BB 
)BB 
;BB 
appDD 
.DD 
UseAuthorizationDD  
(DD  !
)DD! "
;DD" #
appFF 
.FF 
UseEndpointsFF 
(FF 
	endpointsFF &
=>FF' )
{GG 
	endpointsHH 
.HH 
MapControllersHH (
(HH( )
)HH) *
;HH* +
}II 
)II 
;II 
}JJ 	
}KK 
}LL €
jD:\number-recognition-net\number-recognition-api\NumberRecognitionAPI\NumberRecognitionAPI\Utils\Shared.cs
	namespace 	 
NumberRecognitionAPI
 
. 
Utils $
{ 
public 

static 
class 
Shared 
{ 
public		 
static		 
async		 
Task		  
<		  !
byte		! %
[		% &
]		& '
>		' ( 
IFormFileToByteArray		) =
(		= >
	IFormFile		> G
image		H M
)		M N
{

 	
Memory 
< 
byte 
> 
image_bytes $
=% &
new' *
Memory+ 1
<1 2
byte2 6
>6 7
(7 8
new8 ;
byte< @
[@ A
imageA F
.F G
LengthG M
]M N
)N O
;O P
await 
image 
. 
OpenReadStream &
(& '
)' (
.( )
	ReadAsync) 2
(2 3
image_bytes3 >
)> ?
;? @
return 
image_bytes 
. 
ToArray &
(& '
)' (
;( )
} 	
public 
static 
( 
bool 
, 
object "
)" #
CheckIfIsValidImage$ 7
(7 8
	IFormFile8 A
imageB G
)G H
{ 	
object 
response 
; 
if 
( 
image 
== 
null 
||  
!! "
image" '
.' (
ContentType( 3
.3 4
Contains4 <
(< =
$str= D
)D E
)E F
{ 
response 
= 
new 
{ 
status 
= 
$str $
,$ %
message 
= 
$str 9
} 
; 
return 
( 
false 
, 
response '
)' (
;( )
} 
if 
( 
image 
. 
Length 
> 
$num &
)& '
{ 
response 
= 
new 
{ 
status   
=   
$str   $
,  $ %
message!! 
=!! 
$str!! S
}"" 
;"" 
return## 
(## 
false## 
,## 
response## '
)##' (
;##( )
}$$ 
return%% 
(%% 
true%% 
,%% 
null%% 
)%% 
;%%  
}&& 	
}'' 
}(( 