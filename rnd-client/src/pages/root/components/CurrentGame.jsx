import {Box, Button, Typography} from "@mui/material";

export default function CurrentGame () {
  return (
    <Box height={170} padding={1} display="flex" justifyContent="space-between" alignItems="flex-end" sx={{backgroundImage: `url(https://cdn.discordapp.com/attachments/1104404469090881556/1113971170581155900/c0a31dc92f19f998.png)`, backgroundSize: "Cover", backgroundPosition: "center", backgroundRepeat: "no-repeat"}}>
      <Button color="neutral" sx={{flexDirection: "column", alignItems: "flex-start"}}>
        <Typography variant="caption" component="p">
          {game.owner}
        </Typography>
        <Typography variant="body2">
          {game.title}
        </Typography>
      </Button>
      <Button>
        Сменить
      </Button>
    </Box>
  );
}

const game = new Game("Мрак", "AndrewSolo", "https://cdn.discordapp.com/attachments/1104404469090881556/1113971170581155900/c0a31dc92f19f998.png");

function Game(title, owner, image) {
  return {title, owner, image};
}