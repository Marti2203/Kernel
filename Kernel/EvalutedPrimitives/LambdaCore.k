;$lambda
($define! $lambda
($vau (formals . body) env
(wrap (eval (list* $vau formals #ignore body)
env))))