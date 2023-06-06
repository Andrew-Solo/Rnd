import {Avatar, Box, Button, Typography} from "@mui/material";

export default function AccountBanner () {
  return (
    <Button variant="text" color="neutral" sx={{height: 80, padding: 0}}>
      <Box height={1} width={1} display="flex" gap="8px" justifyContent="center" alignItems="center" sx={{background: "rgba(255, 255, 255, 0.1)"}}>
        <Typography variant="h4">
          {user.title}
        </Typography>
        <Avatar alt={user.title} src={user.image}/>
      </Box>
    </Button>
  );
}

const user = {title: "AndrewSolo", image: "https://cdn.discordapp.com/attachments/1104404469090881556/1114280466275635361/486b66aea66db656.png"}