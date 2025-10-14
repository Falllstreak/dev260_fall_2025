// Compare your measured times to your Part 1 predictions. Where do the results clearly reflect Big-O expectations?

- The results from the benchmark mostly overlapped with the predictions. Both Dictionary.ContainsKey and HashSet.Contains both stayed very low for all the various N values tested. This matched the prediction and the expectation for O(1) averages.


// Any surprises? (e.g., why List.Contains might be “fast enough” for small N; constant factors; JIT effects; your machine variability.)

- I think the small differences stuck out to me. It's interesting to see the different timings each time the benchmark is run. It looks like this could just be part of machine variability and it makes sense that the tests need to be run a few times to capture a good idea of what it's saying.

// Given a large dataset and many membership checks, which structure would you choose and why?

- I think the timings on HashSets have been impressive for the larger data sets. It handles large data very well and looks to scale very well to it. Some of the other options start to slow down as it searches linearly through large amounts of data.
