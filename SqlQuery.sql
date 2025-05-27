SELECT   gestionale_quadri.commesse.commessa,
         gestionale_quadri.commesse.anno,
         gestionale_quadri.commesse.azienda,
         gestionale_quadri.commesse.indice_commessa,
         gestionale_quadri.commesse.nome_commessa,
         gestionale_quadri.commesse.matricola_commessa,
         gestionale_quadri.commesse.note AS note_commessa,
         gestionale_quadri.aziende.mrc,
         gestionale_quadri.commesse.data_ciclo_lavoro
         --,quadri.*
FROM     gestionale_quadri.commesse
         INNER JOIN
         gestionale_quadri.quadri
         ON gestionale_quadri.quadri.commessa = gestionale_quadri.commesse.commessa
         INNER JOIN
         gestionale_quadri.aziende
         ON gestionale_quadri.commesse.azienda = gestionale_quadri.aziende.azienda
WHERE    gestionale_quadri.commesse.ciclo_lavoro = 'Y'
         AND gestionale_quadri.commesse.anno = '2025'
         AND gestionale_quadri.commesse.azienda = '1'
ORDER BY commesse.indice_commessa DESC;