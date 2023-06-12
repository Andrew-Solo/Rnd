import {Card, CardActionArea, CardContent, Stack, Typography} from "@mui/material";

export default function ItemCard({name, image, title, subtitle}) {
  return (
    <Card sx={{height: 1, minHeight: 200, minWidth: 200, borderRadius: "8px", background: `url(${image}) no-repeat center`, backgroundSize: "cover"}}>
      <CardActionArea href={name} sx={{height: 1}}>
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