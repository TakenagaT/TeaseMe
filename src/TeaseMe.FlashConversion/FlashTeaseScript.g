grammar FlashTeaseScript;

options {
    language=CSharp3;
    TokenLabelType=CommonToken;
	output=AST;
	ASTLabelType=CommonTree;
}


tokens {
	ACTION;
	BUTTON;
	BUTTONS;
	CAP;
	DELAY;
	FROM;
	GO;
	HIDDEN;
	ID;
	MAX;
	MIN;
	MULT;
	NO;
	NORMAL;
	PAGE;
	PIC;
	PREFIX;
	PROPERTIES;
	RANDOM;
	RANGE;
	SECRET;
	SET;
	SOUND;
	STYLE;
	TARGET;
	TEXT;
	TIME;
	TO;
	UNSET;
	YES;
	YN;
}

@lexer::namespace{TeaseMe.FlashConversion}
@parser::namespace{TeaseMe.FlashConversion}

@members {
ArrayList exceptions = new ArrayList();

public override void ReportError(RecognitionException e)
{
    exceptions.Add(e);
}

public bool HasError
{
	get { return exceptions.Count > 0; }
}

public string ErrorMessage
{
	get { return this.GetErrorMessage(exceptions[0] as RecognitionException, this.TokenNames); }
}

public string ErrorPosition
{
	get { return this.GetErrorHeader(exceptions[0] as RecognitionException); }
}
}

@header {
using System.Collections;
using System.Text;
}

public tease
	:	page*
	;

page
	:	pageRef 'page(' pageProperties? ')' 
		-> ^(PAGE pageRef ^(PROPERTIES pageProperties)?)
	;

pageProperties
	:	pageProp (',' pageProp)* 
		-> pageProp+
	;

pageProp
	:	textDef
		-> ^(TEXT textDef) 
	|	actionDef
	;

textDef
	:	'text:'! QUOTED_STRING
	;

actionDef
	:	actionPrefix! (actionMult | actionHorVert | actionPic | actionSound | actionGo | actionYn | actionDelay | actionButtons | actionUnset | actionSet)?
	;

actionMult
	:	'mult(' actionDef (',' actionDef)* ')'	
		-> actionDef+
	;

actionHorVert
	:	'vert(' actionDef (',' actionDef)* ')'	
		-> actionDef+
	|	'horiz(' actionDef (',' actionDef)* ')'	
		-> actionDef+
	;

actionPrefix
	:	'action:'
	|	'instruc:'
	|	'hidden:'
	|	'media:'
	|	'e' ( options {greedy=false;} :  ~(':'))* ':'
	;

actionPic
	:	'pic(id:' QUOTED_STRING ')'
		-> ^(PIC QUOTED_STRING) 
	;

actionSound
	:	'sound(id:' QUOTED_STRING ')'
		-> ^(SOUND QUOTED_STRING)
	;

actionGo
	:	'go(' targetDef ')'
		-> ^(GO targetDef)
	;

actionYn
	:	'yn(' yesDef ',' noDef ')'
		-> ^(YN ^(YES yesDef) ^(NO noDef))
	;


yesDef
	:	'yes:'! ( pageRef | rangeDef )
	;


noDef
	:	'no:'! ( pageRef | rangeDef )
	;

actionDelay
	:	'delay(' timeDef (',' targetDef)? (',' styleDef)? ')'
		-> ^(DELAY ^(TIME timeDef) ^(TARGET targetDef) ^(STYLE styleDef?) )
	;

actionButtons
	:	'buttons(' buttonSeq ')'
		-> ^(BUTTONS buttonSeq)
	;

buttonSeq
	:	buttonDef (',' buttonDef)*
		-> buttonDef+
	;

buttonDef
	:	'target' INTEGER ':' pageRef ',cap' INTEGER ':' QUOTED_STRING
		-> ^(BUTTON ^(TARGET pageRef) ^(CAP QUOTED_STRING))
	;

actionUnset
	:	'unset(' actionList ')'
		-> ^( UNSET actionList )
	;

actionSet
	:	'set(' actionList ')'
		-> ^( SET actionList )
	;

actionList
	:	actionApply (',' actionApply)*	
		-> actionApply+
	;

actionApply
	:	(actionId ':')? pageRef
		-> pageRef
	;

actionId
	:	'action' INTEGER
	;

targetDef
	:	'target:'! ( pageRef | rangeDef )
	;

timeDef
	:	'time:random('! timeRange ')'!
	|	'time:' timeValue 
		-> ^(MIN timeValue)
	;

timeRange
	:	MIN ':' timeValue ',' MAX ':' timeValue
		-> ^(MIN timeValue) ^(MAX timeValue) 
	;

timeValue
	:	INTEGER timeUnit?
	;

MAX	:	'max';
MIN	:	'min';

timeUnit
	:	'sec' | 'min' | 'hrs'
	;

styleDef
	:	('style:normal' | 'style:\'normal\'') -> NORMAL
	|	('style:hidden' | 'style:\'hidden\'') -> HIDDEN
	|	('style:secret' | 'style:\'secret\'') -> SECRET
	;

NORMAL	: 'normal';
HIDDEN	: 'hidden';
SECRET	: 'secret';



rangeDef
	:	'range(' 'from:' INTEGER ',' 'to:' INTEGER ')'
		-> ^(RANGE ^(FROM INTEGER) ^(TO INTEGER))	
	|	'range(' 'from:' INTEGER ',' 'to:' INTEGER ',' 'prefix'? ':'? QUOTED_STRING ')'
		-> ^(RANGE ^(FROM INTEGER) ^(TO INTEGER) ^(PREFIX QUOTED_STRING))	
	;


pageRef
	:	pageId '#'?	
		-> ^(ID pageId)
	;

pageId
	:	(INTEGER|LETTERS)+
	;


QUOTED_STRING
	:	'"' ( options {greedy=false;} :  ~('"'|'\n'|'\r'))* '"'
	|	'\'' ( options {greedy=false;} :  ~('\''|'\n'|'\r'))* '\''
	|	'’' ( options {greedy=false;} :  ~('’'|'\n'|'\r'))* '’'
	;

INTEGER
	:	('0'..'9')+
	;

LETTERS
	:	('a'..'z' | 'A'..'Z' )+
	;

WS
	:	(' '|'\r'|'\t'|'\u000C'|'\n') { Skip(); }
	;

