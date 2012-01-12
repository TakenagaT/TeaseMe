grammar FlashTeaseScript;

options {
    language=CSharp3;
    TokenLabelType=CommonToken;
	output=AST;
	ASTLabelType=CommonTree;
}


tokens {
	PAGE;
	ID;
	PROPERTIES;
	TEXT;
	ACTION;
	PIC;
	SOUND;
	TARGET;
	GO;
	YN;
	DELAY;
	YES;
	NO;
	TIME;
	SEC;
	MIN;
	STYLE;
	NORMAL;
	HIDDEN;
	SECRET;
	RANGE;
	FROM;
	TO;
	PREFIX;
	MULT;
	UNSET;
	SET;
	CAP;
	BUTTONS;
	BUTTON;
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
	:	'text:'! STRING
	;

actionDef
	:	actionPrefix! (actionMult | actionVert | actionPic | actionSound | actionGo | actionYn | actionDelay | actionButtons | actionUnset | actionSet)?
	;

actionMult
	:	'mult(' actionDef (',' actionDef)* ')'	
		-> actionDef+
	;

actionVert
	:	'vert(' actionDef (',' actionDef)* ')'	
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
	:	'pic(id:' STRING ')'
		-> ^(PIC STRING) 
	;

actionSound
	:	'sound(id:' STRING ')'
		-> ^(SOUND STRING)
	;

actionGo
	:	'go(' targetDef ')'
		-> ^(GO targetDef)
	;

actionYn
	:	'yn(' 'yes:' pageRef ','  'no:' pageRef ')'
		-> ^(YN ^(YES pageRef) ^(NO pageRef))
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
	:	'target' INTEGER ':' pageRef ',cap' INTEGER ':' STRING
		-> ^(BUTTON ^(TARGET pageRef) ^(CAP STRING))
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
	:	actionId! ':'! pageRef
	;

actionId
	:	'action' INTEGER
	;

targetDef
	:	'target:'! ( pageRef | rangeDef )
	;

timeDef
	:	'time:'! INTEGER (SEC|MIN)
	;

SEC		: 'sec';
MIN		: 'min';

styleDef
	:	'style:'! (NORMAL | HIDDEN | SECRET)
	;

NORMAL	: 'normal';
HIDDEN	: 'hidden';
SECRET	: 'secret';

rangeDef
	:	'range(' 'from:' INTEGER ',' 'to:' INTEGER ')'
		-> ^(RANGE ^(FROM INTEGER) ^(TO INTEGER))	
	|	'range(' 'from:' INTEGER ',' 'to:' INTEGER ',' ':'? STRING ')'
		-> ^(RANGE ^(FROM INTEGER) ^(TO INTEGER) ^(PREFIX STRING))	
	;


pageRef
	:	pageId '#'?	
		-> ^(ID pageId)
	;

pageId
	:	(INTEGER|LETTERS)+
	;


STRING
	:	'"' ( options {greedy=false;} :  ~('"'|'\n'|'\r'))* '"'
	|	'\'' ( options {greedy=false;} :  ~('\''|'\n'|'\r'))* '\''
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

