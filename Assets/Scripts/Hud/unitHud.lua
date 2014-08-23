function OnStart()
	AddBase("base",20,20,220,GameHeight-40)
	AddSelectedPortrait("sPortrait",40,65)
	AddText("name",40,40,SelectedUnitName)
	AddText("attackText",40,200,"Attack:")
	AddText("defenceText",40,250,"Defence:")
end

function OnUpdateValues()
	SetText("name", SelectedUnitName)
end

function OnShow()

	Pop("base",15)
	Pop("sPortrait",20)
	
	--Showing texts
	FadeIn("name",25,20)
	FadeIn("attackText",25,20)
	FadeIn("defenceText",25,20)
	
	--Loading and showing tokens
	AddTokens("attack",40,210,180,Selected.Attack)
	FadeIn("attack",25,20)
	AddTokens("defence",40,260,180,Selected.Defence)
	FadeIn("defence",25,20)
end

function OnHide()
	FadeOut("base",20,20)
	FadeOut("sPortrait",15,10)
	
	--Hiding texts
	FadeOut("name",15,10)
	FadeOut("attackText",15,10)
	FadeOut("defenceText",15,10)
	
	--Hiding and destroying tokens
	FadeOutAndKill("attack",15,10)
	FadeOutAndKill("defence",15,10)
end