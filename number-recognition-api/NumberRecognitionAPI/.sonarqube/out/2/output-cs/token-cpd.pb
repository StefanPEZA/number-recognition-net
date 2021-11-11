“$
‰C:\Users\stefa\Documents\GitHub\fraud-detection-net\number-recognition-api\NumberRecognitionAPI\Services\DatasetService\DatasetService.cs
	namespace 	
Services
 
. 
DatasetService !
{ 
public		 

class		 
DatasetService		 
:		  !
IDatasetService		" 1
{

 
private 
readonly 
IRepository $
<$ %
Dataset% ,
>, -
_repository. 9
;9 :
public 
DatasetService 
( 
IRepository )
<) *
Dataset* 1
>1 2

repository3 =
)= >
{ 	
_repository 
= 

repository $
;$ %
} 	
public 
async 
Task 
< 
IEnumerable %
<% &
Dataset& -
>- .
>. /
GetAllDatasetAsync0 B
(B C
intC F
labelG L
,L M
intN Q
limitR W
=X Y
$numZ \
)\ ]
{ 	
return 
await 
_repository $
.$ %
GetAllAsync% 0
(0 1
limit1 6
,6 7
d8 9
=>: <
d= >
.> ?
Label? D
==E G
labelH M
)M N
;N O
} 	
public 
async 
Task 
< 
IEnumerable %
<% &
Dataset& -
>- .
>. /"
GetAllTestDatasetAsync0 F
(F G
intG J
labelK P
,P Q
intR U
limitV [
=\ ]
$num^ `
)` a
{ 	
return 
await 
_repository $
.$ %
GetAllAsync% 0
(0 1
limit1 6
,6 7
d8 9
=>: <
d= >
.> ?
IsTest? E
&&F H
dI J
.J K
LabelK P
==Q S
labelT Y
)Y Z
;Z [
} 	
public 
async 
Task 
< 
IEnumerable %
<% &
Dataset& -
>- .
>. /#
GetAllTrainDatasetAsync0 G
(G H
intH K
labelL Q
,Q R
intS V
limitW \
=] ^
$num_ a
)a b
{ 	
return 
await 
_repository $
.$ %
GetAllAsync% 0
(0 1
limit1 6
,6 7
d8 9
=>: <
!= >
d> ?
.? @
IsTest@ F
&&G I
dJ K
.K L
LabelL Q
==R T
labelU Z
)Z [
;[ \
} 	
public   
async   
Task   
<   
Dataset   !
>  ! "
GetDatasetAsync  # 2
(  2 3
Guid  3 7
id  8 :
)  : ;
{!! 	
return"" 
await"" 
_repository"" $
.""$ %
GetByIdAsync""% 1
(""1 2
id""2 4
)""4 5
;""5 6
}## 	
public%% 
async%% 
Task%% 
InsertIntoDataset%% +
(%%+ ,
Dataset%%, 3
dataset%%4 ;
)%%; <
{&& 	
await'' 
_repository'' 
.'' 
InsertAsync'' )
('') *
dataset''* 1
)''1 2
;''2 3
}(( 	
public** 
async** 
Task** 
UpdateDataset** '
(**' (
Dataset**( /
dataset**0 7
)**7 8
{++ 	
await,, 
_repository,, 
.,, 
UpdateAsync,, )
(,,) *
dataset,,* 1
),,1 2
;,,2 3
}-- 	
public// 
async// 
Task// "
DeleteFromDatasetAsync// 0
(//0 1
Guid//1 5
id//6 8
)//8 9
{00 	
Dataset11 
dataset11 
=11 
await11 #
_repository11$ /
.11/ 0
GetByIdAsync110 <
(11< =
id11= ?
)11? @
;11@ A
await22 
_repository22 
.22 
DeleteAsync22 )
(22) *
dataset22* 1
)221 2
;222 3
}33 	
}44 
}55 ñ
ŠC:\Users\stefa\Documents\GitHub\fraud-detection-net\number-recognition-api\NumberRecognitionAPI\Services\DatasetService\IDatasetService.cs
	namespace 	
Services
 
. 
DatasetService !
{ 
public 

	interface 
IDatasetService $
{		 
Task

 
<

 
IEnumerable

 
<

 
Dataset

  
>

  !
>

! "
GetAllDatasetAsync

# 5
(

5 6
int

6 9
label

: ?
,

? @
int

A D
limit

E J
=

K L
$num

M O
)

O P
;

P Q
Task 
< 
IEnumerable 
< 
Dataset  
>  !
>! ""
GetAllTestDatasetAsync# 9
(9 :
int: =
label> C
,C D
intE H
limitI N
=O P
$numQ S
)S T
;T U
Task 
< 
IEnumerable 
< 
Dataset  
>  !
>! "#
GetAllTrainDatasetAsync# :
(: ;
int; >
label? D
,D E
intF I
limitJ O
=P Q
$numR T
)T U
;U V
Task 
< 
Dataset 
> 
GetDatasetAsync %
(% &
Guid& *
id+ -
)- .
;. /
Task 
InsertIntoDataset 
( 
Dataset &
dataset' .
). /
;/ 0
Task 
UpdateDataset 
( 
Dataset "
dataset# *
)* +
;+ ,
Task "
DeleteFromDatasetAsync #
(# $
Guid$ (
id) +
)+ ,
;, -
} 
} 