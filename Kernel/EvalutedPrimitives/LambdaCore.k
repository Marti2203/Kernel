;$lambda-core
($define! $lambda-core
($vau (formals . body) env
(wrap (eval (list* $vau formals #ignore body)
env))))