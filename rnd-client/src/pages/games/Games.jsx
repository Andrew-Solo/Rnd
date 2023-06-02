import {Typography, Card, Grid, CardContent, CardActionArea, Box} from "@mui/material";

export default function Games () {
  return (
    <Grid container spacing={2}>
      {gamesData.map(game => (
        <Grid item xs="12" sm="6" md="3">
          <Card sx={{height: "100%", width: "100%", minHeight: 100, minWidth: 200, backgroundImage: `url(${game.image})`, backgroundSize: "Cover", backgroundPosition: "center", backgroundRepeat: "no-repeat"}}>
            <CardActionArea>
              <CardContent>
                {/*Save proportions on responsive*/}
                <Box sx={{minHeight: 30}}/>
                <Typography variant="caption" align="right" component="p">
                  {game.owner}
                </Typography>
                <Typography variant="body2" align="right">
                  {game.title}
                </Typography>
              </CardContent>
            </CardActionArea>
          </Card>
        </Grid>
      ))}
    </Grid>
  )
}

const gamesData = [
  new Game("Мрак", "AndrewSolo", "https://cdn.discordapp.com/attachments/1104404469090881556/1113971170581155900/c0a31dc92f19f998.png"),
  new Game("Сказки Латаифа", "Doktor", "https://cdn.discordapp.com/attachments/1104404469090881556/1113971208950648862/avatar.jpg"),
];

function Game(title, owner, image) {
  return {title, owner, image};
}