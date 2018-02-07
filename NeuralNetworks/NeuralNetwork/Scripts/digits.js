;var digits = {};

(function () {
    var self = this;
    var canvas = 'canvas';
    var fileId = '#file';
    var squerSize = 10;

    self.initialize = function () {
        self.initialCanvas();
        $(fileId).change(self.readFile);
       

    };

    self.readFile = function (evt) {
        var reader = new FileReader();
        var file = evt.target.files[0];
        var reader = new FileReader();
        if (file != undefined) {
            reader.readAsBinaryString(file);
        }
        reader.onload = function () {
            //var binaryString = arrayBufferToString(reader.result);
            console.log(reader.result);
        };
    };

    self.initialCanvas = function () {
        var c = document.getElementById("canvas");
        var ctx = c.getContext("2d");
        // x, y
        ctx.fillRect(0, 10, squerSize, squerSize);
    };

    function BinaryToString(binary) {
        var error;

        try {
            return decodeURIComponent(escape(binary));
        } catch (_error) {
            error = _error;
            if (error instanceof URIError) {
                return binary;
            } else {
                throw error;
            }
        }
    }

    function arrayBufferToString(buffer) {
        return BinaryToString(String.fromCharCode.apply(null, Array.prototype.slice.apply(new Uint8Array(buffer))));
    }

}).apply(digits);