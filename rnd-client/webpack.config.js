const path = require("path");
const HtmlWebpackPlugin = require('html-webpack-plugin');

module.exports = {
  devServer: {
    hot: true,
    historyApiFallback: true
  },
  entry: './src/index.tsx',
  devtool: 'source-map',
  output: {
    path: path.resolve(__dirname, 'build'),
    filename: 'rnd.bundle.js'
  },
  resolve: {
    extensions: ['.ts', '.tsx', '.js', '.json']
  },
  module: {
    rules: [
      {
        test: /\.tsx?$/,
        use: ['babel-loader'],
        exclude: /node_modules/,
      },
      {
        test: /\.(jpg|png|svg)$/,
        use: [
          {
            loader: 'url-loader',
            options: {
              limit: 8192
            }
          }
        ]
      },
      {
        test: /\.(less|css)$/,
        use: [
          {
            loader: 'css-loader'
          },
          {
            loader: 'less-loader',
            options: {
              sourceMap: true,
              lessOptions: {
                javascriptEnabled: true
              }
            }
          }
        ]
      }
    ]
  },
  plugins: [
    new HtmlWebpackPlugin({
      title: 'Admin Dashboard Template',
      filename: 'index.html',
      template: './src/index.html',
      inject: true,
      hash: true,
      path: './'
    })
  ]
};
