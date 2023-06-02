import {Box, Card, CardActionArea, CardContent, Stack, Typography} from "@mui/material";

export default function HeroCard({image, title, subtitle}) {
  return (
    <Card sx={{height: 1, minHeight: 200, minWidth: 200, borderRadius: "8px", backgroundImage: `url(${image})`, backgroundSize: "Cover", backgroundPosition: "center", backgroundRepeat: "no-repeat"}}>
      <CardActionArea  sx={{height: 1}}>
        <CardContent sx={{height: 1, background: "linear-gradient(180deg, rgba(0, 0, 0, 0) 0%, rgba(0, 0, 0, 0.4) 100%)"}}>
          <Stack height={1} justifyContent="flex-end">
            {/*Save proportions on responsive*/}
              <Typography variant="caption" align="right" component="p">
                {subtitle}
              </Typography>
              <Typography variant="body2" align="right">
                {title}
              </Typography>
          </Stack>
        </CardContent>
      </CardActionArea>
    </Card>
  );
}