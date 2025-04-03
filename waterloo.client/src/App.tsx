import { useState } from "react";
import AnimatedButton from "./components/AnimatedButton";
import ErrorDisplay from "./components/ErrorDisplay";
import { ErrorResponse } from "./types/errorTypes";

export default function App() {
    const [targetUrl, setTargetUrl] = useState("");
    const [keywords, setKeywords] = useState("");
    const [located, setLocated] = useState("");
    const [isScraping, setIsScraping] = useState(false);
    const [error, setError] = useState<ErrorResponse|null>(null);

    const handleScrape = async () => {
        setIsScraping(true);
        setLocated("");

        try {
            setError(null);
            const response = await fetch("/scrape", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify({ targetUrl, keywords }),
            });

            if (!response.ok) {
                console.log(response)
                setError(await response.json())
                throw new Error("Failed to scrape");
            } else {
                const data = await response.json();
                setLocated(data.positions?.length == 0 ? "Nothing found!" : data.positions.join(", "));
            }

        } catch (err) {
            console.log(err);
        } finally {
            setIsScraping(false);
        }
    };

    return (
        <div className="mx-auto flex h-screen max-w-lg flex-col items-center justify-start space-y-4 p-6">
            <h1 id="tableLabel">SEO Google Search</h1>
            <p>Search <a href='https://www.google.co.uk'>Google</a> to find positions of target URL.</p>
            <div className="flex w-full space-x-2">
                <div className="flex flex-grow flex-col">
                    <label htmlFor="targetUrl" className="mb-1 font-medium">Target URL</label>
                    <input
                        id="targetUrl"
                        type="text"
                        value={targetUrl}
                        onChange={(e) => setTargetUrl(e.target.value)}
                        className="p-2 border rounded-lg"
                    />
                </div>
                <div className="flex flex-grow flex-col">
                    <label htmlFor="keywords" className="mb-1 font-medium">Keywords</label>
                    <input
                        id="keywords"
                        type="text"
                        value={keywords}
                        onChange={(e) => setKeywords(e.target.value)}
                        className="p-2 border rounded-lg"
                    />
                </div>

            </div>
            <div>
                <div className="flex flex-grow flex-col">
                    <AnimatedButton
                        onClick={handleScrape}
                        disabled={isScraping}
                        busy={isScraping}
                        className={`w-100 h-full p-2 rounded-lg ${isScraping ? "bg-gray-500" : "bg-blue-500 hover:bg-blue-600"}`}
                    >
                        Click here to perform keywords search...
                    </AnimatedButton>
                </div>
            </div>
            {!error && located && (<div className="w-full">
                <label htmlFor="located" className="mb-1 block font-medium">Positions where Target URL found</label>
                <input
                    id="located"
                    type="text"
                    value={located}
                    readOnly
                    className="w-full rounded-lg border bg-gray-100 p-2"
                />
            </div>)}
            <div>
                <ErrorDisplay error={error} />
            </div>
        </div>
    );
}
