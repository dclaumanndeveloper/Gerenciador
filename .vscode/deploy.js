// Node.js API provided by Visual Studio Code: https://nodejs.org/en/docs
const Path = require('path');

// Visual Studio Code API: https://code.visualstudio.com/docs/extensionAPI/vscode-api
const vscode = require('vscode');


// entry point
exports.execute = function(args) {
    // args: https://mkloubert.github.io/vscode-deploy-reloaded/interfaces/_plugins_script_.scriptarguments.html

    // module shipped with extension: https://github.com/mkloubert/vscode-deploy-reloaded/blob/master/package.json
    const FSExtra = args.require('fs-extra');
    // module of extension: https://github.com/mkloubert/vscode-deploy-reloaded/tree/master/src
    const Helpers = args.require('./helpers');

    if (0 == args.operation)
        // Delete files
        return deleteFiles(args);  // s. below

    if (1 == args.operation)
        // Deploy / upload
        return deployFiles(args);  // s. below

    if (2 == args.operation)
        // list directory
        return listDirectory(args);  // s. below

    if (3 == args.operation)
        // Pull / download
        return pullFiles(args);  // s. below

    throw new Error(args.operation + ' operation is not supported!');
};


// DELETE
async function deleteFiles(args) {
    for (let file of args.files) {
        // file: https://mkloubert.github.io/vscode-deploy-reloaded/interfaces/_plugins_.filetodelete.html

        if (args.isCancelling)
            break;  // user wants to cancel

        try {
            await file.onBeforeDelete();  // tell that we are going to start the
                                          // delete operation for this file now
                                          // 
                                          // you can submit an optional string that
                                          // is displayed as 'destination' in the GUI

            // do the delete operation here
            throw new Error('Not implemented!');

            await file.onDeleteCompleted();  // tell that anything worked fine
        }
        catch (e) {
            await file.onDeleteCompleted(e);  // submit the error
        }
    }
}

// DEPLOY / UPLOAD
async function deployFiles(args) {
    for (let file of args.files) {
        // file: https://mkloubert.github.io/vscode-deploy-reloaded/interfaces/_plugins_.filetoupload.html

        if (args.isCancelling)
            break;  // user wants to cancel

        try {
            await file.onBeforeUpload();  // tell that we are going to start the
                                          // deploy operation for this file now
                                          // 
                                          // you can submit an optional string that
                                          // is displayed as 'destination' in the GUI

            let contentToDeploy = await Promise.resolve( file.read() );

            throw new Error('Not implemented!');

            await file.onUploadCompleted();  // tell that anything worked fine
        }
        catch (e) {
            await file.onUploadCompleted(e);  // submit the error
        }
    }
}

// LIST DIRECTORY
async function listDirectory(args) {
    let result = {
        dirs: [],   // DirectoryInfo: https://mkloubert.github.io/vscode-deploy-reloaded/interfaces/_files_.directoryinfo.html
        files: [],  // FileInfo: https://mkloubert.github.io/vscode-deploy-reloaded/interfaces/_files_.fileinfo.html
        others: [],  // other FileSystemInfo objects: https://mkloubert.github.io/vscode-deploy-reloaded/interfaces/_files_.filesysteminfo.html
        target: args.target
    };

    // the directory to list is stored in
    // 'args.dir'

    // args.isCancelling provides if
    // user wants to cancel or not

    return result;
}

// PULL / DOWNLOAD
async function pullFiles(args) {
    for (let file of args.files) {
        // file: https://mkloubert.github.io/vscode-deploy-reloaded/interfaces/_plugins_.filetodownload.html

        if (args.isCancelling)
            break;  // user wants to cancel

        try {
            await file.onBeforeDownload();  // tell that we are going to start the
                                            // pull operation for this file now
                                            // 
                                            // you can submit an optional string that
                                            // is displayed as 'source' in the GUI

            // do the pull operation here
            // 
            // we store the data in 'downloadedData' var
            // for this example
            // 
            // recommended is to load the data as buffer
            // or readable NodeJS stream
            throw new Error('Not implemented!');

            // tell that anything worked fine
            // and submit the data to write
            await file.onDownloadCompleted(null, downloadedData);
        }
        catch (e) {
            await file.onDownloadCompleted(e);  // submit the error
        }
    }
}
