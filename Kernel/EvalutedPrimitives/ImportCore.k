($define! $import!
($vau (exp . symbols) env
(eval (list $set!
env
symbols
(cons list symbols))
(eval exp env))))