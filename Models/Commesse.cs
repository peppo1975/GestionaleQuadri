using System.Text.RegularExpressions;
using Microsoft.AspNetCore.SignalR.Protocol;

namespace GestionaleQuadri.Models
{
    public class Commesse : Generale
    {
        public string Anno { get; set; } = string.Empty;
        public string azienda { get; set; } = string.Empty;
        public string nome_azienda { get; set; } = string.Empty;

        public static List<Commesse> AnnoList()
        {
            List<Commesse> list = new List<Commesse>();

            string query = "SELECT commesse.anno, aziende.azienda, aziende.nome_azienda FROM gestionale_quadri.commesse \r\nINNER JOIN gestionale_quadri.aziende ON gestionale_quadri.aziende.azienda = gestionale_quadri.commesse.azienda\r\n\r\nGROUP BY gestionale_quadri.commesse.anno, gestionale_quadri.aziende.nome_azienda , gestionale_quadri.aziende.azienda\r\nORDER BY gestionale_quadri.commesse.anno DESC, gestionale_quadri.aziende.nome_azienda ASC\r\n";

            list = Database.SELECT_GET_LIST<Commesse>(query);

            return list;
        }



        public static List<AziendeAnno> AziendeAnno(string Anno)
        {
            List<AziendeAnno> list = new List<AziendeAnno>();

            string query = $"   SELECT   gestionale_quadri.aziende.azienda," +
                           $"         gestionale_quadri.aziende.nome_azienda " +
                           $"   FROM     gestionale_quadri.commesse" +
                           $"   INNER JOIN gestionale_quadri.aziende" +
                           $"   ON gestionale_quadri.aziende.azienda = gestionale_quadri.commesse.azienda " +
                           $"   WHERE    gestionale_quadri.commesse.anno = '{Anno}' " +
                           $"   GROUP BY gestionale_quadri.aziende.azienda, gestionale_quadri.aziende.nome_azienda;";

            list = Database.SELECT_GET_LIST<AziendeAnno>(query);

            return list;
        }

        public static List<CommesseTable> CommesseAziendaAnno(string Anno, string Azienda)
        {
            List<CommesseAzienda> commesseAzienda = new List<CommesseAzienda>();

            string query = $" SELECT   gestionale_quadri.commesse.commessa, " +
                            $"          gestionale_quadri.commesse.anno, " +
                            $"          gestionale_quadri.commesse.azienda, " +
                            $"          gestionale_quadri.commesse.indice_commessa, " +
                            $"          gestionale_quadri.commesse.nome_commessa, " +
                            $"          gestionale_quadri.commesse.matricola_commessa, " +
                            $"          gestionale_quadri.commesse.note AS note_commessa, " +
                            $"          gestionale_quadri.aziende.mrc, " +
                            $"          gestionale_quadri.commesse.data_ciclo_lavoro, " +
                            $"          gestionale_quadri.quadri.quadro, " +
                            $"          gestionale_quadri.quadri.indice_quadro, " +
                            $"          gestionale_quadri.quadri.nome_quadro, " +
                            $"          gestionale_quadri.quadri.data_inserimento, " +
                            $"          FORMAT(gestionale_quadri.commesse.data_ciclo_lavoro,'yyyy-MM-dd HH-mm-ss') as 'data_ciclo_lavoro_service' " +
                            $" FROM     gestionale_quadri.commesse " +
                            $"          INNER JOIN " +
                            $"          gestionale_quadri.quadri " +
                            $"          ON gestionale_quadri.quadri.commessa = gestionale_quadri.commesse.commessa " +
                            $"          INNER JOIN " +
                            $"          gestionale_quadri.aziende " +
                            $"          ON gestionale_quadri.commesse.azienda = gestionale_quadri.aziende.azienda " +
                            $" WHERE    gestionale_quadri.commesse.ciclo_lavoro = 'Y' " +
                            $"          AND gestionale_quadri.commesse.anno = '{Anno}' " +
                            $"          AND gestionale_quadri.commesse.azienda = '{Azienda}' " +
                            $" ORDER BY commesse.indice_commessa DESC; ";


            commesseAzienda = Database.SELECT_GET_LIST<CommesseAzienda>(query);

            List<CommesseTable> response = CommesseTable(commesseAzienda);

            return response;
        }


        private static List<CommesseTable> CommesseTable(List<CommesseAzienda> commesseAzienda)
        {
            List<CommesseTable> commesseTable = new List<CommesseTable>();

            //List<InfoCommessa> commesse = commesseAzienda.Select(x=>new InfoCommessa {commessa = x.commessa, nome_commessa = x.nome_commessa }).Distinct().ToList();
            //var clientiID = Storico.DistinctBy(x => x.CKY_CNT).ToList();
            //List<InfoCommessa> commesse = commesseAzienda.DistinctBy(x=>x.commessa).Select(x => new InfoCommessa { commessa = x.commessa, nome_commessa = x.nome_commessa }).ToList();
            List<InfoCommessa> commesse = commesseAzienda.Select(x => new InfoCommessa { commessa = x.commessa, nome_commessa = x.nome_commessa, data_ciclo_lavoro = x.data_ciclo_lavoro, data_ciclo_lavoro_service = x.data_ciclo_lavoro_service }).DistinctBy(x => x.commessa).ToList();
            var f = commesse.DistinctBy(x => x.commessa).ToList();
            foreach (InfoCommessa item in commesse)
            {
                CommesseTable commessa = new CommesseTable();
                List<InfoQuadri> quadri = commesseAzienda.Where(x => x.commessa == item.commessa).Select(p => new InfoQuadri { nome_quadro = p.nome_quadro, data_inserimento = p.data_inserimento }).ToList();
                commessa.commessa = item.commessa;
                commessa.nome_commessa = item.nome_commessa;
                commessa.data_ciclo_lavoro = item.data_ciclo_lavoro;
                commessa.commessa_data_ciclo_lavoro_service = item.nome_commessa + " " + item.data_ciclo_lavoro_service;
                commessa.quadri = quadri;
                commesseTable.Add(commessa);
            }

            return commesseTable;
        }


    }



    public abstract class AziendeAnno
    {
        public string Anno { get; set; } = String.Empty;
        public string azienda { get; set; } = string.Empty;
        public string nome_azienda { get; set; } = string.Empty;
    }

    public class CommesseAzienda
    {
        public string commessa { get; set; } = string.Empty;
        public string anno { get; set; } = string.Empty;
        public string azienda { get; set; } = string.Empty;
        public string indice_commessa { get; set; } = string.Empty;
        public string nome_commessa { get; set; } = string.Empty;
        public string matricola_commessa { get; set; } = string.Empty;
        public string note_commessa { get; set; } = string.Empty;
        public string mrc { get; set; } = string.Empty;
        public string data_ciclo_lavoro { get; set; } = string.Empty;
        public string nome_quadro { get; set; } = string.Empty;
        public string data_inserimento { get; set; } = string.Empty;
        public string data_ciclo_lavoro_service { get; set; } = string.Empty;


    }


    public class InfoCommessa
    {
        public string commessa { get; set; } = string.Empty;
        public string nome_commessa { get; set; } = string.Empty;
        public string data_ciclo_lavoro { get; set; } = string.Empty;
        public string data_ciclo_lavoro_service { get; set; } = string.Empty;
    }

    public class CommesseTable : InfoCommessa
    {
        // public string commessa { get; set; } = string.Empty;
        // public string nome_commessa { get; set; } = string.Empty;
        // public string data_ciclo_lavoro { get; set; } = string.Empty;

        public string commessa_data_ciclo_lavoro_service { get; set; } = string.Empty;
        public List<InfoQuadri> quadri { get; set; } = new List<InfoQuadri>();
    }

    public class InfoQuadri
    {
        public string nome_quadro { get; set; } = string.Empty;
        public string data_inserimento { get; set; } = string.Empty;
    }

}