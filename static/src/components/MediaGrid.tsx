import { useEffect, useState } from 'react';
import {
  makeStyles,
  createStyles,
  Theme
} from '@material-ui/core/styles';
import {
  Grid,
  CircularProgress
} from '@material-ui/core';
import MediaPost from './MediaPost';
import { apiBaseUrl } from '../services/api';
import MediaPostModel from '../models/mediaPostModel';


const useStyles = makeStyles((_: Theme) =>
    createStyles({
        grid: {
            margin: '0 19px 0 19px'
        },
        progress: {
            position: 'absolute',
            width: '50px', height: '50px',
            top: '50%', left: '50%',
            margin: '-25px 0 0 -25px'
        }
    }),
);


function MediaGrid() {
    const accountGuid = '00000000-0000-0000-0000-000000000000';
    const classes = useStyles();

    const [postsLoaded, setPostsLoaded] = useState(false);
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
        setPostsLoaded(true);
      }

      setPosts([]);
      setPostsLoaded(false);
      fetchMediaPosts();
    }, []);

    return (
      <div className={classes.grid}>
        {postsLoaded ? (
          <Grid container>{posts}</Grid>
        ) : (
          <div className={classes.progress}>
            <CircularProgress />
          </div>
        )}
      </div>
    );
}

export default MediaGrid;
