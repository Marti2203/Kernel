;copy-es-immutable
($define! copy-es-immutable ($lambda (object) (((wrap $vau) () #ignore (cons (unwrap list) object)))))