const PROXY_CONFIG = [
  {
    context: [
      "/api/livros",
    ],
    //target: "https://localhost:7134",
    target: "https://localhost:44343",
    secure: false
  }
]

module.exports = PROXY_CONFIG;
