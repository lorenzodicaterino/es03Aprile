const stampa = () => {
  $.ajax({
    url: "http://localhost:5195/api/prodotto",
    type: "GET",
    success: function (risultato) {
      let contenuto = "";
      for (let [idx, item] of risultato.entries()) {
        contenuto += `
            <tr>
            <td>${item.titolo}</td>
            <td>${item.descrizione}</td>
            <td>${item.categoria}</td>
            <td>${item.prezzo}</td>
            <td>${item.quantita}</td>
            </tr>
            `;
      }

      $("#corpo-tabella").html(contenuto);
    },
    error: function (errore) {
      alert("ERROREEEE");
      console.log(errore);
    },
  });
};

const salva = () => {
  let nomen = $("#nome").val();
  let desc = $("#descrizione").val();
  let prez = $("#prezzo").val();
  let quan = $("#quantita").val();
  let cate = $("#categoria").val();

  $.ajax({
    url: "http://localhost:5195/api/prodotto",
    type: "POST",
    data: JSON.stringify({
      nomen: nome,
      descrizione: desc,
      prezzo: prez,
      quantita: quan,
      categoria: cate,
    }),
    contentType: "application/json",
    dataType: "json",
    success: function () {
      alert("INSERITO");
      stampa();
    },
    error: function (errore) {
      alert("ERROREEEE");
      console.log(errore);
    },
  });
};

$(document).ready(function () {
  stampa();
  $(".salva").on("click", () => {
    salva();
  });
});
