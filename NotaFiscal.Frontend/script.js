const url = "http://localhost:5139/api/NotaFiscal";
const urlBusca = "http://localhost:5139/buscarid/";
let notas = [];

function listNotas() {
    fetch(url)
        .then(response => response.json())
        .then(data => _displayItems(data))
        .catch(error => console.error("Nota não encontrada.", error))
}

function addNota() {
    const numero = document.getElementById("numero").value;
    const valor = document.getElementById("valor").value;
    const data = document.getElementById("data").value;
    const cnpjEmissor = document.getElementById("cnpjEmissor").value;
    const cnpjDestinatario = document.getElementById("cnpjDestinatario").value;

    const nota = {
        Numero: numero,
        Valor: valor,
        Data: data,
        CnpjEmissor: cnpjEmissor,
        CnpjDestinatario: cnpjDestinatario,
    };

    fetch(url, {
        method: "POST",
        headers: { 
            "Accept": "application/json",
            "Content-type": "application/json"  
        },
        body: JSON.stringify(nota)
    })
        .then(response => response.json())
        .then(() => {
            listNotas();
        })
        .catch(error => console.error("Não foi possível adicionar a nota.", error))
}