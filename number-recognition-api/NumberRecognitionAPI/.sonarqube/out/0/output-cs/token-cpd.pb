�
{C:\Users\stefa\Documents\GitHub\fraud-detection-net\number-recognition-api\NumberRecognitionAPI\Domain\Models\BaseEntity.cs
	namespace 	
Domain
 
. 
Models 
{ 
public 

class 

BaseEntity 
{ 
public 
Guid 
Id 
{ 
get 
; 
set !
;! "
}# $
} 
}		 �
xC:\Users\stefa\Documents\GitHub\fraud-detection-net\number-recognition-api\NumberRecognitionAPI\Domain\Models\Dataset.cs
	namespace 	
Domain
 
. 
Models 
{ 
public 

class 
Dataset 
: 

BaseEntity %
{ 
public 
string 
Label 
{ 
get !
;! "
set# &
;& '
}( )
public 
byte 
[ 
] 
ImageMatrix !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
bool 
IsTest 
{ 
get  
;  !
set" %
;% &
}' (
} 
}		 