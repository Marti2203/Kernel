($define! $let-core*
($vau (bindings . body) env
(eval ($if (null? bindings)
(list* $let bindings body)
(list $let
(list (car bindings))
(list* $let* (cdr bindings) body)))
env)))