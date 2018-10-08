($define! $bindings->environment-core
($vau bindings denv
(eval (list $let-redirect
(make-environment)
bindings
(list get-current-environment))
denv)))