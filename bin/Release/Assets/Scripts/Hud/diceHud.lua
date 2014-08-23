function OnStart()
	AddBase("base",GameWidth/2-80,120,160,100)
end

function OnUpdateValues()

end

function OnShow()
	NotDone()

	Pop("base",10)
	FadeIn("attack",25,20)
	
	AddTokenSpin("spin",GameWidth/2-16,125)
	FadeIn("spin",25,20)
	AddButton("ok","Attack!",GameWidth/2-50,180,100,20,"OnHit")
	FadeIn("ok",25,20)
end

function OnHit()
	StopToken("spin", Result, 30)
	Call("OnHide",80)
end

function OnHide()
	Shrink("base",20)
	FadeOut("attack",25,20)
	
	FadeOutAndKill("ok",25,20)
	FadeOutAndKill("spin",25,20)
	Call("HideComplete",60)
end

function HideComplete()
	Done()
end