($define! or-core?
($lambda x
(not? (apply and? (map not? x)))))