import { useEffect, useState } from 'react';
import {
  makeStyles,
  createStyles,
  Theme
} from '@material-ui/core/styles';
import {
  Grid
} from '@material-ui/core';
import MediaPost from './MediaPost';
import { apiBaseUrl } from '../services/api';
import MediaPostModel from '../models/mediaPostModel';


const useStyles = makeStyles((_: Theme) =>
    createStyles({
        grid: {
            margin: '0 19px 0 19px'
        }
    }),
);


function MediaGrid() {
    const accountGuid = '00000000-0000-0000-0000-000000000000';
    const classes = useStyles();

    const [posts, setPosts] = useState([] as JSX.Element[]);

    useEffect(() => {
      async function fetchMediaPosts() {
        const mediaPosts = [] as JSX.Element[];

        let response = await fetch(apiBaseUrl + `/api/MediaPost?accountGuid=${accountGuid}`, { mode: 'cors' });
        let postIds = await response.json() as { mediaPostIds: string[] };

        for (let id of postIds.mediaPostIds) {
          response = await fetch(apiBaseUrl + `/api/MediaPost?postGuid=${id}`, { mode: 'cors' });
          let postView = await response.json() as MediaPostModel
          mediaPosts.push(<MediaPost
                            key={postView.id}
                            src={apiBaseUrl + `/api/MediaPost?postGuid=${id}&onlyMedia=true`}
                            title={postView.title}
                            account={postView.account}
                            views={postView.views}
                            createdAt={new Date(postView.createdAt)} />);
        }

        setPosts(mediaPosts);
      }

      setPosts([]);
      fetchMediaPosts();
    }, []);

    return (
      <div className={classes.grid}>
        <Grid container>{posts}</Grid>
      </div>
    );
}

export default MediaGrid;
