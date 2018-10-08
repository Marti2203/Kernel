($define! $provide-core!
($vau (symbols . body) env
(eval (list $define! symbols
(list $let ()
(list* $sequence body)
(list* list symbols)))
env)))