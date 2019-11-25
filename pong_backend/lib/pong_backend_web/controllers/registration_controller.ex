defmodule PongBackendWeb.RegistrationController do
  use PongBackendWeb, :controller
  alias PongBackend.Repo
  alias PongBackend.Accounts.Player

  def sign_up(conn, %{"name" => name}) do
    changeset = Player.changeset(%Player{}, %{"name" => name, "wins" => 0, "losses" => 0})

    case Repo.insert(changeset) do
      {:ok, player} ->
        {:ok, jwt, _full_claims} = PongBackend.Guardian.encode_and_sign(player, %{}, permissions: %{player: []})
        {:ok, _} = Redix.command(RedixConnection, ["ZADD", "leaderboard", 0, player.id])
        conn
        |> put_status(:created)
        |> render("success.json", player: player, jwt: jwt)
      {:error, changeset} ->
        conn
        |> put_status(:unprocessable_entity)
        |> render(PongBackendWeb.ChangesetView, "error.json", changeset: changeset)
    end
  end
end
