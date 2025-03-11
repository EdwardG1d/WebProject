const path = require('path');

module.exports = {
    outputDir: path.resolve(__dirname, '../wwwroot/dist'),
    publicPath: '/dist/', // Важно для правильной работы роутинга и ссылок
    // Дополнительные настройки webpack, если необходимо
};