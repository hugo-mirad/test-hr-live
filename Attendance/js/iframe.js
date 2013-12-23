Sys.Application.add_init(AppInit);

function AppInit() {
  var prm = Sys.WebForms.PageRequestManager.getInstance();
  prm.add_endRequest(InitializeRequest);
}

function InitializeRequest(sender, args) {
debugger
  // Check to be sure this async postback is actually
  //   requesting the file download.
  if (sender._postBackSettings.sourceElement.id == "lnkDwnloadPDF") {
    // Create an IFRAME.
    var iframe = document.createElement("iframe");

    // Get the desired region from the dropdown.
    var filepath = $get("hdnPayrollPdf").value;
     //alert(filepath);
    // Point the IFRAME to GenerateFile, with the
    //   desired region as a querystring argument.
    
    iframe.src = "PayRollDownLoad.aspx";

    // This makes the IFRAME invisible to the user.
    iframe.style.display = "none";

    // Add the IFRAME to the page.  This will trigger
    //   a request to GenerateFile now.
    document.body.appendChild(iframe);
  
  }
}