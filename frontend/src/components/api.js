async function request(url, method, body) {
    const response = await fetch(url, {
        method,
        body,
        headers: {
            ...{
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
        }
    });
    try
    {
        const json = await response.json();
        if (!response.ok)
            throw json;
        return json;
    }
    catch(e)
    {
        return;
    }
}

export const getDeck = (id) => request(`/api/decks/${id}`, "GET");
export const createDeck = (name, cards) => request('/api/decks', 'POST',
    JSON.stringify({
        name: name,
        cards: cards,
    }));

export const editDeck = (id, name, cards, deleteCards) => request(`/api/decks/${id}`, 'PATCH',
    JSON.stringify({
        name: name,
        cards: cards,
        deleteCards: deleteCards
    }));

export const deleteDeck = (id) => request(`/api/decks/${id}`, 'DELETE');
export const getDecks = () => request("/api/decks", "GET");

