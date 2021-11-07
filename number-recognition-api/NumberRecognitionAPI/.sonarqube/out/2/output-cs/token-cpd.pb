—
‰C:\Users\stefa\Documents\GitHub\fraud-detection-net\number-recognition-api\NumberRecognitionAPI\Services\DatasetService\DatasetService.cs
	namespace		 	
Services		
 
.		 
DatasetService		 !
{

 
public 

class 
DatasetService 
:  !
IDatasetService" 1
{ 
private 
readonly 
IRepository $
<$ %
Dataset% ,
>, -
_repository. 9
;9 :
public 
DatasetService 
( 
IRepository )
<) *
Dataset* 1
>1 2

repository3 =
)= >
{ 	
_repository 
= 

repository $
;$ %
} 	
public 
async 
Task 
< 
IEnumerable %
<% &
Dataset& -
>- .
>. /"
GetAllTestDatasetAsync0 F
(F G
)G H
{ 	
return 
await 
_repository $
.$ %
GetAllAsync% 0
(0 1
d1 2
=>3 5
d6 7
.7 8
IsTest8 >
)> ?
;? @
} 	
public 
async 
Task 
< 
IEnumerable %
<% &
Dataset& -
>- .
>. /#
GetAllTrainDatasetAsync0 G
(G H
)H I
{ 	
return 
await 
_repository $
.$ %
GetAllAsync% 0
(0 1
d1 2
=>3 5
!6 7
d7 8
.8 9
IsTest9 ?
)? @
;@ A
} 	
public 
async 
Task 
< 
Dataset !
>! "
GetDatasetAsync# 2
(2 3
Guid3 7
id8 :
): ;
{ 	
return 
await 
_repository $
.$ %
GetByIdAsync% 1
(1 2
id2 4
)4 5
;5 6
}   	
public"" 
void"" 
InsertIntoDataset"" %
(""% &
Dataset""& -
dataset"". 5
)""5 6
{## 	
_repository$$ 
.$$ 
InsertAsync$$ #
($$# $
dataset$$$ +
)$$+ ,
;$$, -
}%% 	
public'' 
void'' 
UpdateDataset'' !
(''! "
Dataset''" )
dataset''* 1
)''1 2
{(( 	
_repository)) 
.)) 
UpdateAsync)) #
())# $
dataset))$ +
)))+ ,
;)), -
}** 	
public,, 
async,, 
void,, "
DeleteFromDatasetAsync,, 0
(,,0 1
Guid,,1 5
id,,6 8
),,8 9
{-- 	
Dataset.. 
dataset.. 
=.. 
await.. #
_repository..$ /
.../ 0
GetByIdAsync..0 <
(..< =
id..= ?
)..? @
;..@ A
_repository// 
.// 
DeleteAsync// #
(//# $
dataset//$ +
)//+ ,
;//, -
}00 	
}11 
}22 À

ŠC:\Users\stefa\Documents\GitHub\fraud-detection-net\number-recognition-api\NumberRecognitionAPI\Services\DatasetService\IDatasetService.cs
	namespace 	
Services
 
. 
DatasetService !
{		 
public

 

	interface

 
IDatasetService

 $
{ 
Task 
< 
IEnumerable 
< 
Dataset  
>  !
>! ""
GetAllTestDatasetAsync# 9
(9 :
): ;
;; <
Task 
< 
IEnumerable 
< 
Dataset  
>  !
>! "#
GetAllTrainDatasetAsync# :
(: ;
); <
;< =
Task 
< 
Dataset 
> 
GetDatasetAsync %
(% &
Guid& *
id+ -
)- .
;. /
void 
InsertIntoDataset 
( 
Dataset &
dataset' .
). /
;/ 0
void 
UpdateDataset 
( 
Dataset "
dataset# *
)* +
;+ ,
void "
DeleteFromDatasetAsync #
(# $
Guid$ (
id) +
)+ ,
;, -
} 
} 