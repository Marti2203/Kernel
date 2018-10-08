($define! $vau-special
($let (($vau $vau)) ; save the primitive
($vau (formals eformal . body) env
(eval (list $vau formals eformal
(cons $sequence body))
env))))