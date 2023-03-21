const PROXY_CONFIG = [
    {
      context: [
        "/api/patients",
      ],
      target: "https://localhost:7217",
      secure: false
    },
    {
      context: [
        "/api/patient",
      ],
      target: "https://localhost:7217",
      secure: false
    }
]
  
  module.exports = PROXY_CONFIG;
  