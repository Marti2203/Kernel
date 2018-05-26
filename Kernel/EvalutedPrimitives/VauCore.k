;$vau-core
($define! $vau
((wrap ($vau ($vau) #ignore
($vau (formals eformal . body) env
(eval (list $vau formals eformal
(cons $sequence body))
env))))
$vau))